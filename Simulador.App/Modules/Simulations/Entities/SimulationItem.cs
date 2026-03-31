namespace Simulador.App.Modules.Simulations.Entities;

public class SimulationItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid SimulationRunId { get; set; }
    public SimulationRun SimulationRun { get; set; } = default!;

    public string Sku { get; set; } = default!;
    public string InternalCode { get; set; } = default!;
    public string Description { get; set; } = default!;

    public int Quantity { get; set; }
    public decimal? UnitPriceInput { get; set; }
    public int? PalletUnitsOverride { get; set; }
}