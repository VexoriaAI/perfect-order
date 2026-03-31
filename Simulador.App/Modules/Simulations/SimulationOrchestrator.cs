using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Simulador.App.Data;
using Simulador.App.Modules.Simulations.Entities;
using Simulador.Engine.Contracts;
using Simulador.Engine.Services;

namespace Simulador.App.Modules.Simulations;

public sealed class SimulationOrchestrator
{
    private readonly AppDbContext _db;
    private readonly SimulationEngine _engine;

    public SimulationOrchestrator(AppDbContext db, SimulationEngine engine)
    {
        _db = db;
        _engine = engine;
    }

    public async Task<(SimulationResult result, Guid runId)> RunAndPersistAsync(SimulationRequest request)
    {
        var config = await _db.SystemConfigs.AsNoTracking()
            .FirstAsync(x => x.Key == "DEFAULT");

        var customer = await _db.Customers.AsNoTracking()
            .Include(x => x.Rules)
            .Include(x => x.AllowedVehicles)
            .FirstOrDefaultAsync(x => x.CustomerCode == request.CustomerCode);

        if (customer is null)
            throw new InvalidOperationException("Customer not found");

        if (!customer.IsActive)
            throw new InvalidOperationException("Customer inactive");

        var vehicleList = await _db.Vehicles.AsNoTracking().ToListAsync();

        var skus = request.Items
            .Select(i => i.Sku)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        var products = await _db.Products.AsNoTracking()
            .Where(p => skus.Contains(p.Sku))
            .ToListAsync();

        var productDict = products.ToDictionary(
            p => p.Sku,
            p => new ProductSnapshot
            {
                InternalCode = p.InternalCode,
                Sku = p.Sku,
                Description = p.Description,
                Unit = p.Unit,
                WeightKg = p.WeightKg,
                VolumeM3 = p.VolumeM3,
                PalletUnitsDefault = p.PalletUnitsDefault,
                PackagingType = p.PackagingType,
                IsActive = p.IsActive,
                DefaultUnitPrice = p.DefaultUnitPrice
            },
            StringComparer.OrdinalIgnoreCase);

        var skuRules = await _db.SkuRules.AsNoTracking()
            .Where(r => skus.Contains(r.Sku))
            .ToListAsync();

        var skuRulesDict = skuRules.ToDictionary(r => r.Sku, r => r, StringComparer.OrdinalIgnoreCase);

        foreach (var sku in skus)
        {
            if (!skuRulesDict.TryGetValue(sku, out var rule))
                throw new InvalidOperationException($"SKU sem regra (valid/inativo): {sku}");

            if (!rule.IsValidForSale || rule.IsInactive)
                throw new InvalidOperationException($"SKU inválido/inativo: {sku}");
        }

        var pricing = new PricingSnapshot();

        if (!request.IsSeller)
        {
            var company = await _db.BillingCompanies.AsNoTracking()
                .FirstOrDefaultAsync(c =>
                    c.CompanyCode == request.CompanyBilling ||
                    c.DisplayName == request.CompanyBilling);

            if (company is null)
                throw new InvalidOperationException($"Empresa de faturamento não encontrada: {request.CompanyBilling}");

            var companyKey = company.DisplayName;

            var prices = await _db.PriceAverages.AsNoTracking()
                .Where(p => p.CompanyBilling == companyKey && skus.Contains(p.Sku))
                .ToListAsync();

            foreach (var p in prices)
                pricing.AveragePrices[(companyKey, p.Sku)] = p.AvgUnitPrice;

            request.CompanyBilling = companyKey;
        }

        var customerSnapshot = new CustomerSnapshot
        {
            CustomerCode = customer.CustomerCode,
            IsActive = customer.IsActive,
            Rules = new CustomerRulesSnapshot
            {
                AllowsBulk = customer.Rules?.AllowsBulk ?? true,
                AllowsPalletized = customer.Rules?.AllowsPalletized ?? true,
                AllowsMix = customer.Rules?.AllowsMix ?? true,
                MaxKgPerPallet = customer.Rules?.MaxKgPerPallet
            },
            AllowedVehicles = customer.AllowedVehicles
                .Select(v => v.VehicleName.Trim().ToUpperInvariant())
                .ToHashSet(StringComparer.OrdinalIgnoreCase)
        };

        var vehiclesSnapshot = vehicleList
            .Select(v => new VehicleSnapshot
            {
                Name = v.Name,
                MaxVolumeM3 = v.MaxVolumeM3,
                MaxWeightKg = v.MaxWeightKg,
                MaxPallets = v.MaxPallets,
                OrderIndex = v.OrderIndex
            })
            .ToList();

        var configSnapshot = new ConfigSnapshot
        {
            DefaultLossPercent = config.DefaultLossPercent,
            FreightIdealPercent = config.FreightIdealPercent,
            FreightAcceptablePercent = config.FreightAcceptablePercent,
            MissingPricePolicy = config.MissingPricePolicy
        };

        var result = _engine.Run(request, customerSnapshot, productDict, vehiclesSnapshot, pricing, configSnapshot);

        var run = new SimulationRun
        {
            CustomerId = customer.Id,
            CompanyBilling = request.CompanyBilling,
            IsSeller = request.IsSeller,
            SellerId = request.SellerId,
            RegionalId = request.RegionalId,
            StateCode = request.StateCode,
            CityName = request.CityName,
            IsPalletized = request.IsPalletized,
            ShipmentType = request.ShipmentType,
            LossPercentApplied = result.Loss.AppliedPercent,
            FreightIdealPercent = request.Freight.IdealPercent ?? configSnapshot.FreightIdealPercent,
            FreightAcceptablePercent = request.Freight.AcceptablePercent ?? configSnapshot.FreightAcceptablePercent,
            RequestJson = JsonSerializer.Serialize(request),
            ResultJson = JsonSerializer.Serialize(result),
            Items = request.Items.Select(i =>
            {
                productDict.TryGetValue(i.Sku, out var product);

                return new SimulationItem
                {
                    Sku = i.Sku,
                    InternalCode = product?.InternalCode ?? string.Empty,
                    Description = product?.Description ?? string.Empty,
                    Quantity = i.Quantity,
                    UnitPriceInput = i.UnitPrice,
                    PalletUnitsOverride = i.PalletUnitsOverride
                };
            }).ToList()
        };

        _db.SimulationRuns.Add(run);
        await _db.SaveChangesAsync();

        return (result, run.Id);
    }
}