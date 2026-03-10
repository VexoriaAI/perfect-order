namespace Simulador.App.Modules.Vehicles.Entities;

public class Vehicle
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
    public decimal MaxVolumeM3 { get; set; }
    public decimal MaxWeightKg { get; set; }
    public int MaxPallets { get; set; }
    public int OrderIndex { get; set; } // menor = menor veículo
}