namespace Simulador.App.Modules.Customers.Entities;

public class CustomerAllowedVehicle
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CustomerId { get; set; }
    public string VehicleName { get; set; } = default!;
}