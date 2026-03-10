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

        // restrição de recebimento (MVP: warning)
        var warnings = new List<string>();
        if (request.IsPalletized && !customer.Rules.AllowsPalletized)
            warnings.Add("CustomerDoesNotAllowPalletized");
        if (!request.IsPalletized && !customer.Rules.AllowsBulk)
            warnings.Add("CustomerDoesNotAllowBulk");

        var itemResults = new List<ItemResult>();
        decimal totalWeight = 0;
        decimal totalVolume = 0;
        int totalPallets = 0;
        decimal totalValue = 0;

        foreach (var item in request.Items)
        {
            if (!products.TryGetValue(item.Sku, out var p) || !p.IsActive)
                throw new InvalidOperationException($"Product not found or inactive: {item.Sku}");

            var qty = item.Quantity;
            var w = qty * p.WeightKg;
            var v = qty * p.VolumeM3;

            var unitsPerPallet = item.PalletUnitsOverride ?? p.PalletUnitsDefault;
            var (basePal, byW, finalPal) = _pallets.Calculate(qty, unitsPerPallet, w, customer.Rules.MaxKgPerPallet);

            decimal unitPriceApplied = 0m;

            if (request.IsSeller)
            {
                if (item.UnitPrice is null)
                    throw new InvalidOperationException($"UnitPrice required for seller item: {item.Sku}");

                unitPriceApplied = item.UnitPrice.Value;
            }
            else
            {
                var key = (request.CompanyBilling, item.Sku);
                if (pricing.AveragePrices.TryGetValue(key, out var avg))
                {
                    unitPriceApplied = avg;
                }
                else
                {
                    if (config.MissingPricePolicy == 1)
                        throw new InvalidOperationException($"Missing average price for {key.CompanyBilling}/{key.Sku}");
                    warnings.Add("PriceAverageMissingForSomeItems");
                    unitPriceApplied = 0m;
                }
            }

            var value = qty * unitPriceApplied;

            itemResults.Add(new ItemResult
            {
                Sku = item.Sku,
                Quantity = qty,
                UnitPriceApplied = unitPriceApplied,
                WeightKg = w,
                VolumeM3 = v,
                PalletsBase = basePal,
                PalletsByWeightLimit = byW,
                PalletsFinal = finalPal
            });

            totalWeight += w;
            totalVolume += v;
            totalPallets += finalPal;
            totalValue += value;
        }

        var lossPct = request.Loss.LossPercent ?? config.DefaultLossPercent;
        if (lossPct < 0 || lossPct > 0.30m) throw new InvalidOperationException("LossPercent out of range");

        var effectiveVolume = request.IsPalletized ? totalVolume : totalVolume * (1 - lossPct);

        var idealPct = request.Freight.IdealPercent ?? config.FreightIdealPercent;
        var accPct = request.Freight.AcceptablePercent ?? config.FreightAcceptablePercent;

        var (freteIdeal, freteAcc) = _freight.Estimate(totalValue, idealPct, accPct);

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
                AcceptableValue = freteAcc
            },
            Vehicle = vehicle,
            Warnings = warnings.Distinct().ToList(),
            Items = itemResults
        };
    }
}