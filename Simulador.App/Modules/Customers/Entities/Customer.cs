namespace Simulador.App.Modules.Customers.Entities;

public class Customer
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string CustomerCode { get; set; } = default!;
    public string Name { get; set; } = default!;
    public bool IsActive { get; set; } = true;

    public string? Uf { get; set; }
    public string? City { get; set; }

    public CustomerRules? Rules { get; set; }
    public List<CustomerAllowedVehicle> AllowedVehicles { get; set; } = new();
}