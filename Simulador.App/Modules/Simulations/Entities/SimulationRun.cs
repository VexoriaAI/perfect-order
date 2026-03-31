namespace Simulador.App.Modules.Simulations.Entities;

public class SimulationRun
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CustomerId { get; set; }

    public string CompanyBilling { get; set; } = default!;
    public bool IsSeller { get; set; }
    public string? SellerId { get; set; }
    public Guid? RegionalId { get; set; }
    public string? StateCode { get; set; }
    public string? CityName { get; set; }

    public bool IsPalletized { get; set; }
    public string ShipmentType { get; set; } = "Dedicated";

    public decimal LossPercentApplied { get; set; }
    public decimal FreightIdealPercent { get; set; }
    public decimal FreightAcceptablePercent { get; set; }

    public string RequestJson { get; set; } = default!;
    public string ResultJson { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<SimulationItem> Items { get; set; } = new();
}