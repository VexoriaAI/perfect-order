namespace Simulador.App.Modules.Catalog.Entities;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Sku { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Unit { get; set; } = "UN";

    public decimal WeightKg { get; set; }
    public decimal VolumeM3 { get; set; }

    public int PalletUnitsDefault { get; set; } // unidades por palete (padrão)
    public string? PackagingType { get; set; }  // CX/FD (opcional)

    public bool IsActive { get; set; } = true;
}