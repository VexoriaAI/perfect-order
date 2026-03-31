namespace Simulador.App.Modules.Regional.DTO;

public sealed class RegionalMini
{
    public Guid Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;

    public string DisplayLabel => $"{Code} - {Name}";
}