namespace Simulador.App.Modules.BillingCompanies.Entities;

public sealed class BillingCompany
{
    public Guid Id { get; set; }
    public string CompanyCode { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public bool IsActive { get; set; } = true;
}