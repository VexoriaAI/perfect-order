namespace Simulador.App.Modules.Customers.Dtos;

public sealed class CustomerCreateDto
{
    public string CustomerCode { get; set; } = default!;
    public string Name { get; set; } = default!;
    public bool IsActive { get; set; } = true;
    public string? Uf { get; set; }
    public string? City { get; set; }

    public CustomerRulesDto? Rules { get; set; }
    public List<string> AllowedVehicles { get; set; } = new();
}