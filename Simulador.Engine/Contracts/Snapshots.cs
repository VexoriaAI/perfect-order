namespace Simulador.Engine.Contracts;

public sealed class ProductSnapshot
{
    public string InternalCode { get; set; } = default!;
    public string Sku { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Unit { get; set; } = default!;
    public decimal WeightKg { get; set; }
    public decimal VolumeM3 { get; set; }
    public int PalletUnitsDefault { get; set; }
    public string? PackagingType { get; set; }
    public bool IsActive { get; set; }
    public decimal? DefaultUnitPrice { get; set; }
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