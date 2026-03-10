namespace Simulador.Engine.Contracts;

public sealed class ProductSnapshot
{
    public string Sku { get; init; } = default!;
    public string Description { get; init; } = default!;
    public string Unit { get; init; } = default!;
    public decimal WeightKg { get; init; }
    public decimal VolumeM3 { get; init; }
    public int PalletUnitsDefault { get; init; }
    public string? PackagingType { get; init; }
    public bool IsActive { get; init; }
}

public sealed class CustomerSnapshot
{
    public string CustomerCode { get; init; } = default!;
    public bool IsActive { get; init; }
    public CustomerRulesSnapshot Rules { get; init; } = new();
    public HashSet<string> AllowedVehicles { get; init; } = new(StringComparer.OrdinalIgnoreCase);
}

public sealed class CustomerRulesSnapshot
{
    public bool AllowsPalletized { get; init; }
    public bool AllowsBulk { get; init; }
    public bool AllowsMix { get; init; }
    public decimal? MaxKgPerPallet { get; init; }
}

public sealed class VehicleSnapshot
{
    public string Name { get; init; } = default!;
    public decimal MaxVolumeM3 { get; init; }
    public decimal MaxWeightKg { get; init; }
    public int MaxPallets { get; init; }
    public int OrderIndex { get; init; }
}

public sealed class PricingSnapshot
{
    // (companyBilling, sku) -> avgUnitPrice
    public Dictionary<(string CompanyBilling, string Sku), decimal> AveragePrices { get; init; }
        = new();
}

public sealed class ConfigSnapshot
{
    public decimal DefaultLossPercent { get; init; }
    public decimal FreightIdealPercent { get; init; }
    public decimal FreightAcceptablePercent { get; init; }
    public int MissingPricePolicy { get; init; }
}