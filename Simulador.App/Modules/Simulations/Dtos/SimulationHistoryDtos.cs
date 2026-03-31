namespace Simulador.App.Modules.Simulations.Dtos;

public sealed class SimulationHistoryRowDto
{
    public Guid RunId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CompanyBilling { get; set; } = default!;
    public bool IsSeller { get; set; }
    public string? SellerId { get; set; }
    public Guid? RegionalId { get; set; }
    public string? StateCode { get; set; }
    public string? CityName { get; set; }
    public int ItemCount { get; set; }
}

public sealed class SimulationHistoryDetailDto
{
    public Guid RunId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CompanyBilling { get; set; } = default!;
    public bool IsSeller { get; set; }
    public string? SellerId { get; set; }
    public Guid? RegionalId { get; set; }
    public string? StateCode { get; set; }
    public string? CityName { get; set; }
    public string RequestJson { get; set; } = default!;
    public string ResultJson { get; set; } = default!;
    public List<SimulationHistoryItemDto> Items { get; set; } = new();
}

public sealed class SimulationHistoryItemDto
{
    public string InternalCode { get; set; } = default!;
    public string Sku { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal? UnitPriceInput { get; set; }
}