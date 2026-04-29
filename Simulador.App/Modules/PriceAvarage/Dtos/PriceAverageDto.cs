namespace Simulador.App.Modules.Catalog.Dtos;

public sealed class PriceAverageDto
{
    public Guid Id { get; set; }
    public string CompanyBilling { get; set; } = default!;
    public string Sku { get; set; } = default!;
    public decimal AvgUnitPrice { get; set; }
    public DateTime UpdatedAt { get; set; }
}