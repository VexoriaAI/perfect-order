namespace Simulador.App.Modules.Catalog.Dtos;

public sealed class PriceAverageUpsertDto
{
    public string CompanyBilling { get; set; } = default!;
    public string Sku { get; set; } = default!;
    public decimal AvgUnitPrice { get; set; }
}