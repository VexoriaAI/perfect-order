using Simulador.Engine.Contracts;

namespace Simulador.Engine.Services;

public sealed class PalletCalculator
{
    public (int basePallets, int byWeight, int finalPallets) Calculate(
        int quantity,
        int unitsPerPallet,
        decimal itemWeightKg,
        decimal? maxKgPerPallet)
    {
        if (unitsPerPallet <= 0) throw new ArgumentException("unitsPerPallet must be > 0");

        var basePallets = (int)Math.Ceiling((decimal)quantity / unitsPerPallet);

        var byWeight = 0;
        if (maxKgPerPallet.HasValue && maxKgPerPallet.Value > 0)
        {
            byWeight = (int)Math.Ceiling(itemWeightKg / maxKgPerPallet.Value);
        }

        var finalPallets = Math.Max(basePallets, byWeight);
        return (basePallets, byWeight, finalPallets);
    }
}