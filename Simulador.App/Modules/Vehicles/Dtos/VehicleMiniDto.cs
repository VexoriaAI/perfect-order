public sealed class VehicleMini
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal MaxVolumeM3 { get; set; }
    public decimal MaxWeightKg { get; set; }
    public int MaxPallets { get; set; }
    public int OrderIndex { get; set; }
}