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
            // effectiveVolumeM3 já vem com perda aplicada pelo Engine
            var ok = isPalletized
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
                    Reason = "Atende à capacidade, mas NÃO é permitido pelo cliente (restrição violada)",
                    CapacityWarning = "Nenhum veículo permitido atende, exibindo a correspondência de capacidade mais próxima"
                };
            }
        }

        if (chosen is null)
        {
            // excedeu tudo
            return new VehicleResult
            {
                Recommended = null,
                Reason = "Nenhum veículo atende à capacidade",
                CapacityWarning = isPalletized
                    ? "Palete e Peso Acima da Capacidade"
                    : "Peso e Cubagem Acima da Capacidade"
            };
        }

        return new VehicleResult
        {
            Recommended = chosen.Name,
            Reason = allowedVehicles.Count > 0
                ? "Atende à capacidade e é permitido pelo cliente"
                : "Atende à capacidade"
        };
    }

    private static string Normalize(string s) => s.Trim().ToUpperInvariant();
}
