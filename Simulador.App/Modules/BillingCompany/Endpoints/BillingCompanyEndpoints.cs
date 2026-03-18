using Microsoft.EntityFrameworkCore;
using Simulador.App.Data;

namespace Simulador.App.Modules.BillingCompanies.Endpoints;

public static class BillingCompanyEndpoints
{
    public static IEndpointRouteBuilder MapBillingCompanyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/billing-companies").WithTags("BillingCompanies")
            .RequireAuthorization();

        group.MapGet("/", async (AppDbContext db) =>
            await db.BillingCompanies
                .AsNoTracking()
                .Where(x => x.IsActive)
                .OrderBy(x => x.CompanyCode)
                .Select(x => new { x.Id, x.CompanyCode, x.DisplayName, x.IsActive })
                .ToListAsync());

        return app;
    }
}