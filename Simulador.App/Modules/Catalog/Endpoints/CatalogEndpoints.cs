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

        group.MapGet("/", async (AppDbContext db) =>
            await db.Products
                .AsNoTracking()
                .OrderBy(p => p.Sku)
                .ToListAsync());

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

        group.MapPut("/{id:guid}", async (Guid id, ProductUpdateDto dto, AppDbContext db) =>
        {
            var product = await db.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product is null)
                return Results.NotFound(new { error = "Produto não encontrado." });

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

            var skuExists = await db.Products.AnyAsync(x => x.Id != id && x.Sku == sku);
            if (skuExists)
                return Results.BadRequest(new { error = $"Já existe outro produto com SKU {sku}." });

            product.InternalCode = internalCode;
            product.Sku = sku;
            product.Description = description;
            product.Unit = unit;
            product.WeightKg = dto.WeightKg;
            product.VolumeM3 = dto.VolumeM3;
            product.PalletUnitsDefault = dto.PalletUnitsDefault;
            product.PackagingType = packagingType;
            product.IsActive = dto.IsActive;
            product.DefaultUnitPrice = dto.DefaultUnitPrice;

            await db.SaveChangesAsync();

            return Results.Ok(new ProductAdminDto
            {
                Id = product.Id,
                InternalCode = product.InternalCode,
                Sku = product.Sku,
                Description = product.Description,
                Unit = product.Unit,
                WeightKg = product.WeightKg,
                VolumeM3 = product.VolumeM3,
                PalletUnitsDefault = product.PalletUnitsDefault,
                PackagingType = product.PackagingType,
                IsActive = product.IsActive,
                DefaultUnitPrice = product.DefaultUnitPrice
            });
        });

        group.MapPatch("/{id:guid}/active", async (Guid id, AppDbContext db) =>
        {
            var product = await db.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product is null)
                return Results.NotFound(new { error = "Produto não encontrado." });

            product.IsActive = !product.IsActive;
            await db.SaveChangesAsync();

            return Results.Ok(new ProductAdminDto
            {
                Id = product.Id,
                InternalCode = product.InternalCode,
                Sku = product.Sku,
                Description = product.Description,
                Unit = product.Unit,
                WeightKg = product.WeightKg,
                VolumeM3 = product.VolumeM3,
                PalletUnitsDefault = product.PalletUnitsDefault,
                PackagingType = product.PackagingType,
                IsActive = product.IsActive,
                DefaultUnitPrice = product.DefaultUnitPrice
            });
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