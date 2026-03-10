namespace Simulador.App.Modules.Customers.Entities;

public class CustomerRules
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = default!;

    public bool AllowsPalletized { get; set; } = true;
    public bool AllowsBulk { get; set; } = true;
    public bool AllowsMix { get; set; } = true;

    public decimal? MaxKgPerPallet { get; set; } // restrição forte para paletes

    public string? SkuPerPalletPolicy { get; set; } // texto/informativo
    public string? PalletType { get; set; }         // informativo
    public decimal? PalletMaxHeight { get; set; }   // informativo

    // custos (opcional)
    public decimal? SchedulingFee { get; set; }
    public decimal? NoShowFee { get; set; }
    public decimal? PalletFee { get; set; }
    public decimal? PalletRedoFee { get; set; }
    public decimal? StretchFee { get; set; }
    public decimal? BulkPerTonFee { get; set; }
    public decimal? BulkPerM3Fee { get; set; }
}