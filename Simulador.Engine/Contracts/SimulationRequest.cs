namespace Simulador.Engine.Contracts;

public class SimulationRequest
{
    public string CustomerCode { get; set; } = default!;
    public string CompanyBilling { get; set; } = default!;

    public bool IsSeller { get; set; }
    public string? SellerId { get; set; }
    public Guid? RegionalId { get; set; }

    public string? StateCode { get; set; }
    public string? CityName { get; set; }

    public bool IsPalletized { get; set; }
    public string ShipmentType { get; set; } = "Dedicated";

    public LossSettings Loss { get; set; } = new();
    public FreightSettings Freight { get; set; } = new();

    public List<SimulationItemRequest> Items { get; set; } = new();
}

public sealed class LossSettings
{
    public string Mode { get; set; } = "Simple";
    public decimal? LossPercent { get; set; }
}

public sealed class FreightSettings
{
    public decimal? IdealPercent { get; set; }
    public decimal? AcceptablePercent { get; set; }
    public decimal? ActualValue { get; set; }
}

public sealed class SimulationItemRequest
{
    public string Sku { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal? UnitPrice { get; set; }
    public int? PalletUnitsOverride { get; set; }
}