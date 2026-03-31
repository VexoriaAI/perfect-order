namespace Simulador.Engine.Contracts;

public class SimulationResult
{
    public Totals Totals { get; set; } = new();
    public LossResult Loss { get; set; } = new();
    public FreightResult Freight { get; set; } = new();
    public VehicleResult Vehicle { get; set; } = new();
    public List<string> Warnings { get; set; } = new();
    public List<ItemResult> Items { get; set; } = new();
}

public sealed class Totals
{
    public decimal WeightKg { get; set; }
    public decimal VolumeM3 { get; set; }
    public int Pallets { get; set; }
    public decimal ItemsValue { get; set; }
}

public sealed class LossResult
{
    public decimal AppliedPercent { get; set; }
    public decimal EffectiveVolumeM3 { get; set; }
}

public sealed class FreightResult
{
    public decimal IdealValue { get; set; }
    public decimal AcceptableValue { get; set; }
    public string Status { get; set; } = "Não informado";
}

public sealed class VehicleResult
{
    public string? Recommended { get; set; }
    public string? Reason { get; set; }
    public string? CapacityWarning { get; set; }
}

public sealed class ItemResult
{
    public string Sku { get; set; } = default!;
    public string InternalCode { get; set; } = default!;
    public string Description { get; set; } = default!;

    public int Quantity { get; set; }
    public decimal UnitPriceApplied { get; set; }
    public decimal WeightKg { get; set; }
    public decimal VolumeM3 { get; set; }
    public int PalletsBase { get; set; }
    public int PalletsByWeightLimit { get; set; }
    public int PalletsFinal { get; set; }
}