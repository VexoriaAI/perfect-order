namespace Simulador.App.Modules.Catalog.Entities;

public sealed class Product
{
    public Guid Id { get; set; }

    public string InternalCode { get; set; } = default!; // ex.: 74617
    public string Sku { get; set; } = default!;          // ex.: SKU001
    public string Description { get; set; } = default!;
    public string Unit { get; set; } = default!;

    public decimal WeightKg { get; set; }
    public decimal VolumeM3 { get; set; }
    public int PalletUnitsDefault { get; set; }

    public string? PackagingType { get; set; }
    public bool IsActive { get; set; }

    public decimal? DefaultUnitPrice { get; set; }
}