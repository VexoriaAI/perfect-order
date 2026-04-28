using Microsoft.EntityFrameworkCore;
using Simulador.App.Data;
using Simulador.App.Modules.BillingCompanies.DTOs;
using Simulador.App.Modules.BillingCompanies.Entities;

namespace Simulador.App.Modules.BillingCompanies.Endpoints;

public static class BillingCompanyEndpoints
{
    public static IEndpointRouteBuilder MapBillingCompanyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/billing-companies")
            .WithTags("BillingCompanies")
            .RequireAuthorization();

            group.MapGet("/", async (AppDbContext db) =>
            {
                var items = await db.BillingCompanies
                    .AsNoTracking()
                    .Select(x => new BillingCompanyMini(
                        x.Id,
                        x.CompanyCode,
                        x.DisplayName,
                        x.IsActive
                    ))
                    .ToListAsync();

                return items
                    .OrderBy(x => int.TryParse(x.CompanyCode, out var n) ? n : int.MaxValue)
                    .ThenBy(x => x.CompanyCode)
                    .ToList();
            });

        group.MapPost("/", async (BillingCompanyCreateDto dto, AppDbContext db) =>
        {
            var companyCode = dto.CompanyCode?.Trim();
            var name = dto.Name?.Trim();

            if (string.IsNullOrWhiteSpace(companyCode))
                return Results.BadRequest(new { error = "Código da empresa é obrigatório." });

            if (string.IsNullOrWhiteSpace(name))
                return Results.BadRequest(new { error = "Nome da empresa é obrigatório." });

            var exists = await db.BillingCompanies
                .AnyAsync(x => x.CompanyCode == companyCode);

            if (exists)
                return Results.BadRequest(new { error = $"Já existe uma empresa com o código {companyCode}." });

            var entity = new BillingCompany
            {
                Id = Guid.NewGuid(),
                CompanyCode = companyCode,
                Name = name,
                DisplayName = $"{companyCode} - {name}",
                IsActive = dto.IsActive
            };

            db.BillingCompanies.Add(entity);
            await db.SaveChangesAsync();

            return Results.Ok(new BillingCompanyMini(
                entity.Id,
                entity.CompanyCode,
                entity.DisplayName,
                entity.IsActive
            ));
        });

        return app;
    }
}