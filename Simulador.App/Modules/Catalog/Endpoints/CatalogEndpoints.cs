using Microsoft.EntityFrameworkCore;
using Simulador.App.Data;
using Simulador.App.Modules.Catalog.Dtos;
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

        // listagem completa para admin
        group.MapGet("/admin", async (AppDbContext db) =>
        {
            var items = await db.Products
                .AsNoTracking()
                .OrderBy(p => p.InternalCode)
                .Select(p => new ProductAdminDto
                {
                    Id = p.Id,
                    InternalCode = p.InternalCode,
                    Sku = p.Sku,
                    Description = p.Description,
                    Unit = p.Unit,
                    WeightKg = p.WeightKg,
                    VolumeM3 = p.VolumeM3,
                    PalletUnitsDefault = p.PalletUnitsDefault,
                    PackagingType = p.PackagingType,
                    IsActive = p.IsActive,
                    DefaultUnitPrice = p.DefaultUnitPrice
                })
                .ToListAsync();

            return Results.Ok(items);
        });

        group.MapPost("/", async (ProductCreateDto dto, AppDbContext db) =>
        {
            var internalCode = dto.InternalCode?.Trim();
            var sku = dto.Sku?.Trim().ToUpperInvariant();
            var description = dto.Description?.Trim();
            var unit = dto.Unit?.Trim().ToUpperInvariant();
            var packagingType = dto.PackagingType?.Trim().ToUpperInvariant();

            if (string.IsNullOrWhiteSpace(internalCode))
                return Results.BadRequest(new { error = "Código interno é obrigatório." });

            if (string.IsNullOrWhiteSpace(sku))
                return Results.BadRequest(new { error = "SKU é obrigatório." });

            if (string.IsNullOrWhiteSpace(description))
                return Results.BadRequest(new { error = "Descrição é obrigatória." });

            if (string.IsNullOrWhiteSpace(unit))
                return Results.BadRequest(new { error = "Unidade é obrigatória." });

            if (dto.WeightKg < 0)
                return Results.BadRequest(new { error = "Peso não pode ser negativo." });

            if (dto.VolumeM3 < 0)
                return Results.BadRequest(new { error = "Cubagem não pode ser negativa." });

            if (dto.PalletUnitsDefault < 0)
                return Results.BadRequest(new { error = "Palete padrão não pode ser negativo." });

            var skuExists = await db.Products.AnyAsync(x => x.Sku == sku);
            if (skuExists)
                return Results.BadRequest(new { error = $"Já existe produto com SKU {sku}." });

            var entity = new Product
            {
                Id = Guid.NewGuid(),
                InternalCode = internalCode,
                Sku = sku,
                Description = description,
                Unit = unit,
                WeightKg = dto.WeightKg,
                VolumeM3 = dto.VolumeM3,
                PalletUnitsDefault = dto.PalletUnitsDefault,
                PackagingType = packagingType,
                IsActive = dto.IsActive,
                DefaultUnitPrice = dto.DefaultUnitPrice
            };

            db.Products.Add(entity);
            await db.SaveChangesAsync();

            return Results.Created($"/api/products/{entity.Id}", new ProductAdminDto
            {
                Id = entity.Id,
                InternalCode = entity.InternalCode,
                Sku = entity.Sku,
                Description = entity.Description,
                Unit = entity.Unit,
                WeightKg = entity.WeightKg,
                VolumeM3 = entity.VolumeM3,
                PalletUnitsDefault = entity.PalletUnitsDefault,
                PackagingType = entity.PackagingType,
                IsActive = entity.IsActive,
                DefaultUnitPrice = entity.DefaultUnitPrice
            });
        });

        // mantém para o simulador
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