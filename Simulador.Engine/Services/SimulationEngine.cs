using Simulador.Engine.Contracts;

namespace Simulador.Engine.Services;

public sealed class SimulationEngine
{
    private readonly PalletCalculator _pallets = new();
    private readonly FreightEstimator _freight = new();
    private readonly VehicleRecommender _vehicles = new();

    public SimulationResult Run(
        SimulationRequest request,
        CustomerSnapshot customer,
        IReadOnlyDictionary<string, ProductSnapshot> products,
        IReadOnlyList<VehicleSnapshot> vehicles,
        PricingSnapshot pricing,
        ConfigSnapshot config)
    {
        if (!customer.IsActive)
            throw new InvalidOperationException("Customer is inactive");

        var warnings = new List<string>();

        if (request.IsPalletized && !customer.Rules.AllowsPalletized)
            warnings.Add("CustomerDoesNotAllowPalletized");

        if (!request.IsPalletized && !customer.Rules.AllowsBulk)
            warnings.Add("CustomerDoesNotAllowBulk");

        var itemResults = new List<ItemResult>();
        decimal totalWeight = 0m;
        decimal totalVolume = 0m;
        int totalPallets = 0;
        decimal totalValue = 0m;

        foreach (var item in request.Items)
        {
            if (!products.TryGetValue(item.Sku, out var p) || !p.IsActive)
                throw new InvalidOperationException($"Product not found or inactive: {item.Sku}");

            var qty = item.Quantity;
            var weight = qty * p.WeightKg;
            var volume = qty * p.VolumeM3;

            var unitsPerPallet = item.PalletUnitsOverride ?? p.PalletUnitsDefault;
            var (basePal, byWeight, finalPal) = _pallets.Calculate(
                qty,
                unitsPerPallet,
                weight,
                customer.Rules.MaxKgPerPallet);

            decimal unitPriceApplied;

            if (request.IsSeller)
            {
                if (!item.UnitPrice.HasValue || item.UnitPrice.Value <= 0)
                    throw new InvalidOperationException($"Preço unitário obrigatório para o SKU {p.Sku}");

                unitPriceApplied = item.UnitPrice.Value;
            }
            else
            {
                var key = (request.CompanyBilling, p.Sku);

                Console.WriteLine($"CompanyBilling request final: {request.CompanyBilling}");
                Console.WriteLine($"SKU: {p.Sku}");
                Console.WriteLine($"Pricing keys loaded: {pricing.AveragePrices.Count}");

                if (pricing.AveragePrices.TryGetValue(key, out var avg) && avg > 0)
                {
                    unitPriceApplied = avg;
                }
                else
                {
                    if (config.MissingPricePolicy == 1)
                        throw new InvalidOperationException($"Missing price for {key.CompanyBilling}/{key.Sku}");

                    warnings.Add("Alguns itens não possuem preço definido. Verifique os detalhes para mais informações.");
                    unitPriceApplied = 0m;
                }
            }

            var value = qty * unitPriceApplied;

            itemResults.Add(new ItemResult
            {
                Sku = p.Sku,
                InternalCode = p.InternalCode,
                Description = p.Description,
                Quantity = qty,
                UnitPriceApplied = unitPriceApplied,
                WeightKg = weight,
                VolumeM3 = volume,
                PalletsBase = basePal,
                PalletsByWeightLimit = byWeight,
                PalletsFinal = finalPal
            });

            totalWeight += weight;
            totalVolume += volume;
            totalPallets += basePal;
            totalValue += value;
        }

        var lossPct = request.Loss.LossPercent ?? config.DefaultLossPercent;
        if (lossPct < 0 || lossPct > 0.30m)
            throw new InvalidOperationException("LossPercent out of range");

        var effectiveVolume = request.IsPalletized
            ? totalVolume
            : totalVolume * (1 - lossPct);

        var idealPct = request.Freight.IdealPercent ?? config.FreightIdealPercent;
        var acceptablePct = request.Freight.AcceptablePercent ?? config.FreightAcceptablePercent;

        var (freteIdeal, freteAceitavel) = _freight.Estimate(totalValue, idealPct, acceptablePct);

        var freightStatus = "Não informado";

        if (request.Freight.ActualValue.HasValue)
        {
            var actual = request.Freight.ActualValue.Value;

            if (actual <= freteIdeal)
                freightStatus = "Ideal";
            else if (actual <= freteAceitavel)
                freightStatus = "Aceitável";
            else
                freightStatus = "Acima do aceitável";
        }

        var vehicle = _vehicles.Recommend(
            request.IsPalletized,
            totalWeight,
            effectiveVolume,
            totalPallets,
            vehicles,
            customer.AllowedVehicles);

        return new SimulationResult
        {
            Totals = new Totals
            {
                WeightKg = totalWeight,
                VolumeM3 = totalVolume,
                Pallets = totalPallets,
                ItemsValue = totalValue
            },
            Loss = new LossResult
            {
                AppliedPercent = request.IsPalletized ? 0m : lossPct,
                EffectiveVolumeM3 = effectiveVolume
            },
            Freight = new FreightResult
            {
                IdealValue = freteIdeal,
                AcceptableValue = freteAceitavel,
                Status = freightStatus
            },
            Vehicle = vehicle,
            Warnings = warnings.Distinct().ToList(),
            Items = itemResults
        };
    }
}