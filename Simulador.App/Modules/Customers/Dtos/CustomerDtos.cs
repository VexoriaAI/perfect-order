namespace Simulador.App.Modules.Customers.Dtos;

public sealed class CustomerDto
{
    public Guid Id { get; set; }
    public string CustomerCode { get; set; } = default!;
    public string Name { get; set; } = default!;
    public bool IsActive { get; set; }
    public string? Uf { get; set; }
    public string? City { get; set; }

    public CustomerRulesDto? Rules { get; set; }
    public List<string> AllowedVehicles { get; set; } = new();
}

public sealed class CustomerRulesDto
{
    public bool AllowsPalletized { get; set; }
    public bool AllowsBulk { get; set; }
    public bool AllowsMix { get; set; }
    public decimal? MaxKgPerPallet { get; set; }

    public string? SkuPerPalletPolicy { get; set; }
    public string? PalletType { get; set; }
    public decimal? PalletMaxHeight { get; set; }

    public decimal? SchedulingFee { get; set; }
    public decimal? NoShowFee { get; set; }
    public decimal? PalletFee { get; set; }
    public decimal? PalletRedoFee { get; set; }
    public decimal? StretchFee { get; set; }
    public decimal? BulkPerTonFee { get; set; }
    public decimal? BulkPerM3Fee { get; set; }
}