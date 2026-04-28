namespace Simulador.App.Modules.BillingCompanies.DTOs;

public record BillingCompanyMini(Guid Id, string CompanyCode, string DisplayName, bool IsActive);

public sealed class BillingCompanyCreateDto
{
    public string CompanyCode { get; set; } = default!;
    public string Name { get; set; } = default!;
    public bool IsActive { get; set; } = true;
}