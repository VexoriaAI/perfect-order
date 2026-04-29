namespace Simulador.App.Modules.Catalog.Dtos;

public sealed class ProductUpdateDto
{
    public string InternalCode { get; set; } = default!;
    public string Sku { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Unit { get; set; } = default!;
    public decimal WeightKg { get; set; }
    public decimal VolumeM3 { get; set; }
    public int PalletUnitsDefault { get; set; }
    public string? PackagingType { get; set; }
    public bool IsActive { get; set; } = true;
    public decimal? DefaultUnitPrice { get; set; }
}