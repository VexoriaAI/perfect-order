using Microsoft.EntityFrameworkCore;
using Simulador.App.Data;
using Simulador.App.Modules.Catalog.Entities;
using Simulador.App.Shared;

namespace Simulador.App.Modules.Catalog.Endpoints;

public static class CatalogEndpoints
{
    public static IEndpointRouteBuilder MapCatalogEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/products")
            .WithTags("Products")
            .RequireAuthorization();

        group.MapGet("/", async (AppDbContext db) =>
            await db.Products
                .AsNoTracking()
                .OrderBy(p => p.Sku)
                .ToListAsync());

        group.MapPost("/", async (Product p, AppDbContext db) =>
        {
            p.Sku = p.Sku.Trim().ToUpperInvariant();
            p.InternalCode = p.InternalCode.Trim();
            p.Description = p.Description.Trim();
            p.Unit = p.Unit.Trim().ToUpperInvariant();

            if (!string.IsNullOrWhiteSpace(p.PackagingType))
                p.PackagingType = p.PackagingType.Trim().ToUpperInvariant();

            db.Products.Add(p);
            await db.SaveChangesAsync();

            return Results.Created($"/api/products/{p.Id}", p);
        });

        group.MapGet("/lookup", async (AppDbContext db) =>
        {
            var items = await db.Products
                .AsNoTracking()
                .OrderBy(p => p.InternalCode)
                .Select(p => new ProductMini
                {
                    Id = p.Id,
                    InternalCode = p.InternalCode,
                    Sku = p.Sku,
                    Description = p.Description,
                    IsActive = p.IsActive
                })
                .ToListAsync();

            return Results.Ok(items);
        });

        var skuGroup = app.MapGroup("/api/sku-rules")
            .WithTags("SkuRules")
            .RequireAuthorization();

        skuGroup.MapGet("/", async (AppDbContext db) =>
            await db.SkuRules
                .AsNoTracking()
                .OrderBy(x => x.Sku)
                .ToListAsync());

        skuGroup.MapPost("/", async (SkuRule r, AppDbContext db) =>
        {
            r.Sku = r.Sku.Trim().ToUpperInvariant();

            db.SkuRules.Add(r);
            await db.SaveChangesAsync();

            return Results.Created($"/api/sku-rules/{r.Id}", r);
        });

        return app;
    }
}