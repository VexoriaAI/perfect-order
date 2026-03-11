using Microsoft.EntityFrameworkCore;
using Simulador.App.Data;
using Simulador.App.Modules.Customers.Dtos;
using Simulador.App.Modules.Customers.Entities;

namespace Simulador.App.Modules.Customers.Endpoints;

public static class CustomerEndpoints
{
    public static IEndpointRouteBuilder MapCustomersEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/customers").WithTags("Customers")
            .RequireAuthorization();

        group.MapGet("/", async (AppDbContext db) =>
        {
            var customers = await db.Customers.AsNoTracking()
                .Include(c => c.Rules)
                .Include(c => c.AllowedVehicles)
                .OrderBy(c => c.CustomerCode)
                .ToListAsync();

            return customers.Select(c => new CustomerDto
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
                AllowedVehicles = c.AllowedVehicles.Select(v => v.VehicleName).ToList()
            });
        });

        group.MapPost("/", async (Customer c, AppDbContext db) =>
        {
            c.CustomerCode = c.CustomerCode.Trim().ToUpperInvariant();
            db.Customers.Add(c);
            await db.SaveChangesAsync();
            return Results.Created($"/api/customers/{c.Id}", c);
        });

        return app;
    }
}
