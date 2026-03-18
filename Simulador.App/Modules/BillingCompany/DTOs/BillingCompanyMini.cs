namespace Simulador.App.Modules.BillingCompanies.DTOs;

public record BillingCompanyMini(Guid Id, string CompanyCode, string DisplayName, bool IsActive);