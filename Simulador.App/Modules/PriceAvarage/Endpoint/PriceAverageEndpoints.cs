using Microsoft.EntityFrameworkCore;
using Simulador.App.Data;
using Simulador.App.Modules.Catalog.Dtos;
using Simulador.App.Modules.Catalog.Entities;

namespace Simulador.App.Modules.Catalog.Endpoints;

public static class PriceAverageEndpoints
{
    public static IEndpointRouteBuilder MapPriceAverageEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/price-averages")
            .WithTags("PriceAverages")
            .RequireAuthorization();

        group.MapGet("/", async (AppDbContext db) =>
        {
            var items = await db.PriceAverages
                .AsNoTracking()
                .OrderBy(x => x.CompanyBilling)
                .ThenBy(x => x.Sku)
                .Select(x => new PriceAverageDto
                {
                    Id = x.Id,
                    CompanyBilling = x.CompanyBilling,
                    Sku = x.Sku,
                    AvgUnitPrice = x.AvgUnitPrice,
                    UpdatedAt = x.UpdatedAt
                })
                .ToListAsync();

            return Results.Ok(items);
        });

        group.MapPost("/", async (PriceAverageUpsertDto dto, AppDbContext db) =>
        {
            var sku = dto.Sku?.Trim().ToUpperInvariant();
            var companyInput = dto.CompanyBilling?.Trim();

            if (string.IsNullOrWhiteSpace(companyInput))
                return Results.BadRequest(new { error = "Empresa de faturamento é obrigatória." });

            if (string.IsNullOrWhiteSpace(sku))
                return Results.BadRequest(new { error = "SKU é obrigatório." });

            if (dto.AvgUnitPrice <= 0)
                return Results.BadRequest(new { error = "Preço médio deve ser maior que zero." });

            var company = await db.BillingCompanies
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.CompanyCode == companyInput ||
                    x.DisplayName == companyInput);

            if (company is null)
                return Results.BadRequest(new { error = $"Empresa não encontrada: {companyInput}" });

            var productExists = await db.Products
                .AsNoTracking()
                .AnyAsync(x => x.Sku == sku);

            if (!productExists)
                return Results.BadRequest(new { error = $"Produto não encontrado para SKU {sku}" });

            var companyKey = company.DisplayName;

            var existing = await db.PriceAverages
                .FirstOrDefaultAsync(x => x.CompanyBilling == companyKey && x.Sku == sku);

            if (existing is null)
            {
                existing = new PriceAverage
                {
                    Id = Guid.NewGuid(),
                    CompanyBilling = companyKey,
                    Sku = sku,
                    AvgUnitPrice = dto.AvgUnitPrice,
                    UpdatedAt = DateTime.UtcNow
                };

                db.PriceAverages.Add(existing);
            }
            else
            {
                existing.AvgUnitPrice = dto.AvgUnitPrice;
                existing.UpdatedAt = DateTime.UtcNow;
            }

            await db.SaveChangesAsync();

            return Results.Ok(new PriceAverageDto
            {
                Id = existing.Id,
                CompanyBilling = existing.CompanyBilling,
                Sku = existing.Sku,
                AvgUnitPrice = existing.AvgUnitPrice,
                UpdatedAt = existing.UpdatedAt
            });
        });

        return app;
    }
}