using Microsoft.EntityFrameworkCore;
using Simulador.App.Data;
using Simulador.App.Modules.Customers.Dtos;
using Simulador.App.Modules.Customers.Entities;

namespace Simulador.App.Modules.Customers.Endpoints;

public static class CustomerEndpoints
{
    public static IEndpointRouteBuilder MapCustomersEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/customers")
            .WithTags("Customers")
            .RequireAuthorization();

        // usado pelo simulador
        group.MapGet("/", async (AppDbContext db) =>
            await db.Customers.AsNoTracking()
                .OrderBy(c => c.CustomerCode)
                .Select(c => new
                {
                    c.CustomerCode,
                    c.Name,
                    c.IsActive
                })
                .ToListAsync());

        // usado pela tela admin
        group.MapGet("/admin", async (AppDbContext db) =>
        {
            var customers = await db.Customers.AsNoTracking()
                .Include(c => c.Rules)
                .Include(c => c.AllowedVehicles)
                .OrderBy(c => c.CustomerCode)
                .ToListAsync();

            return Results.Ok(customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                CustomerCode = c.CustomerCode,
                Name = c.Name,
                IsActive = c.IsActive,
                Uf = c.Uf,
                City = c.City,
                Rules = c.Rules is null ? null : new CustomerRulesDto
                {
                    AllowsBulk = c.Rules.AllowsBulk,
                    AllowsPalletized = c.Rules.AllowsPalletized,
                    AllowsMix = c.Rules.AllowsMix,
                    MaxKgPerPallet = c.Rules.MaxKgPerPallet,
                    SkuPerPalletPolicy = c.Rules.SkuPerPalletPolicy,
                    PalletType = c.Rules.PalletType,
                    PalletMaxHeight = c.Rules.PalletMaxHeight,
                    SchedulingFee = c.Rules.SchedulingFee,
                    NoShowFee = c.Rules.NoShowFee,
                    PalletFee = c.Rules.PalletFee,
                    PalletRedoFee = c.Rules.PalletRedoFee,
                    StretchFee = c.Rules.StretchFee,
                    BulkPerTonFee = c.Rules.BulkPerTonFee,
                    BulkPerM3Fee = c.Rules.BulkPerM3Fee
                },
                AllowedVehicles = c.AllowedVehicles
                    .Select(v => v.VehicleName)
                    .OrderBy(x => x)
                    .ToList()
            }));
        });

        group.MapPost("/", async (CustomerCreateDto dto, AppDbContext db) =>
        {
            var customerCode = dto.CustomerCode?.Trim().ToUpperInvariant();
            var name = dto.Name?.Trim();
            var uf = dto.Uf?.Trim().ToUpperInvariant();
            var city = dto.City?.Trim();

            if (string.IsNullOrWhiteSpace(customerCode))
                return Results.BadRequest(new { error = "Código do cliente é obrigatório." });

            if (string.IsNullOrWhiteSpace(name))
                return Results.BadRequest(new { error = "Nome do cliente é obrigatório." });

            var exists = await db.Customers.AnyAsync(x => x.CustomerCode == customerCode);
            if (exists)
                return Results.BadRequest(new { error = $"Já existe cliente com código {customerCode}." });

            var entity = new Customer
            {
                CustomerCode = customerCode,
                Name = name,
                IsActive = dto.IsActive,
                Uf = uf,
                City = city,
                Rules = dto.Rules is null ? null : new CustomerRules
                {
                    AllowsBulk = dto.Rules.AllowsBulk,
                    AllowsPalletized = dto.Rules.AllowsPalletized,
                    AllowsMix = dto.Rules.AllowsMix,
                    MaxKgPerPallet = dto.Rules.MaxKgPerPallet,
                    SkuPerPalletPolicy = dto.Rules.SkuPerPalletPolicy,
                    PalletType = dto.Rules.PalletType,
                    PalletMaxHeight = dto.Rules.PalletMaxHeight,
                    SchedulingFee = dto.Rules.SchedulingFee,
                    NoShowFee = dto.Rules.NoShowFee,
                    PalletFee = dto.Rules.PalletFee,
                    PalletRedoFee = dto.Rules.PalletRedoFee,
                    StretchFee = dto.Rules.StretchFee,
                    BulkPerTonFee = dto.Rules.BulkPerTonFee,
                    BulkPerM3Fee = dto.Rules.BulkPerM3Fee
                },
                AllowedVehicles = dto.AllowedVehicles
                    .Where(v => !string.IsNullOrWhiteSpace(v))
                    .Select(v => new CustomerAllowedVehicle
                    {
                        VehicleName = v.Trim()
                    })
                    .ToList()
            };

            db.Customers.Add(entity);
            await db.SaveChangesAsync();

            return Results.Created($"/api/customers/{entity.Id}", new CustomerDto
            {
                Id = entity.Id,
                CustomerCode = entity.CustomerCode,
                Name = entity.Name,
                IsActive = entity.IsActive,
                Uf = entity.Uf,
                City = entity.City,
                Rules = entity.Rules is null ? null : new CustomerRulesDto
                {
                    AllowsBulk = entity.Rules.AllowsBulk,
                    AllowsPalletized = entity.Rules.AllowsPalletized,
                    AllowsMix = entity.Rules.AllowsMix,
                    MaxKgPerPallet = entity.Rules.MaxKgPerPallet,
                    SkuPerPalletPolicy = entity.Rules.SkuPerPalletPolicy,
                    PalletType = entity.Rules.PalletType,
                    PalletMaxHeight = entity.Rules.PalletMaxHeight,
                    SchedulingFee = entity.Rules.SchedulingFee,
                    NoShowFee = entity.Rules.NoShowFee,
                    PalletFee = entity.Rules.PalletFee,
                    PalletRedoFee = entity.Rules.PalletRedoFee,
                    StretchFee = entity.Rules.StretchFee,
                    BulkPerTonFee = entity.Rules.BulkPerTonFee,
                    BulkPerM3Fee = entity.Rules.BulkPerM3Fee
                },
                AllowedVehicles = entity.AllowedVehicles
                    .Select(v => v.VehicleName)
                    .OrderBy(x => x)
                    .ToList()
            });
        });

        return app;
    }
}