namespace Simulador.Engine.Services;

public sealed class FreightEstimator
{
    public (decimal ideal, decimal acceptable) Estimate(decimal itemsValue, decimal idealPct, decimal acceptablePct)
    {
        var ideal = itemsValue * idealPct;
        var acceptable = itemsValue * acceptablePct;
        return (ideal, acceptable);
    }
}