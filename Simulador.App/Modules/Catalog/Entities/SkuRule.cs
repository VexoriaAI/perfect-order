namespace Simulador.App.Modules.Catalog.Entities;

public class SkuRule
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Sku { get; set; } = default!;
    public bool IsValidForSale { get; set; } = true;
    public bool IsInactive { get; set; } = false;
}