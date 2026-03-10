using Simulador.Engine.Contracts;

namespace Simulador.Engine.Services;

public sealed class VehicleRecommender
{
    public VehicleResult Recommend(
        bool isPalletized,
        decimal weightKg,
        decimal effectiveVolumeM3,
        int pallets,
        IReadOnlyList<VehicleSnapshot> vehicles,
        HashSet<string> allowedVehicles)
    {
        var ordered = vehicles.OrderBy(v => v.OrderIndex).ToList();

        IEnumerable<VehicleSnapshot> candidates = ordered;
        if (allowedVehicles.Count > 0)
            candidates = ordered.Where(v => allowedVehicles.Contains(Normalize(v.Name)));

        VehicleSnapshot? chosen = null;

        foreach (var v in candidates)
        {
            var ok = isPalletized
                ? (weightKg <= v.MaxWeightKg && pallets <= v.MaxPallets)
                : (weightKg <= v.MaxWeightKg && effectiveVolumeM3 <= v.MaxVolumeM3 * 0.85m); // NOTA: não usar isso aqui

            // IMPORTANTE:
            // Aqui não aplicar 0.85 fixo. O effectiveVolumeM3 já vem com perda aplicada.
            ok = isPalletized
                ? (weightKg <= v.MaxWeightKg && pallets <= v.MaxPallets)
                : (weightKg <= v.MaxWeightKg && effectiveVolumeM3 <= v.MaxVolumeM3);

            if (ok) { chosen = v; break; }
        }

        // Se falhou com restrição de allowedVehicles, tenta sem restrição (pra te dar warning explícito)
        if (chosen is null && allowedVehicles.Count > 0)
        {
            foreach (var v in ordered)
            {
                var ok = isPalletized
                    ? (weightKg <= v.MaxWeightKg && pallets <= v.MaxPallets)
                    : (weightKg <= v.MaxWeightKg && effectiveVolumeM3 <= v.MaxVolumeM3);

                if (ok) { chosen = v; break; }
            }

            if (chosen is not null)
            {
                return new VehicleResult
                {
                    Recommended = chosen.Name,
                    Reason = "Meets capacity, but NOT allowed by customer (restriction violated)",
                    CapacityWarning = "No allowed vehicle fits; showing closest capacity match"
                };
            }
        }

        if (chosen is null)
        {
            // excedeu tudo
            return new VehicleResult
            {
                Recommended = null,
                Reason = "No vehicle fits capacity",
                CapacityWarning = isPalletized
                    ? "Palete e Peso Acima da Capacidade"
                    : "Peso e Cubagem Acima da Capacidade"
            };
        }

        return new VehicleResult
        {
            Recommended = chosen.Name,
            Reason = allowedVehicles.Count > 0
                ? "Meets capacity and is allowed by customer"
                : "Meets capacity"
        };
    }

    private static string Normalize(string s) => s.Trim().ToUpperInvariant();
}