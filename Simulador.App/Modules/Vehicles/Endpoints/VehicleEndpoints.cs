using Microsoft.EntityFrameworkCore;
using Simulador.App.Data;
using Simulador.App.Modules.Vehicles.Entities;

namespace Simulador.App.Modules.Vehicles.Endpoints;

public static class VehicleEndpoints
{
    public static IEndpointRouteBuilder MapVehiclesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/vehicles").WithTags("Vehicles")
            .RequireAuthorization();

        group.MapGet("/", async (AppDbContext db) =>
            await db.Vehicles.AsNoTracking().OrderBy(v => v.OrderIndex).ToListAsync());

        group.MapPost("/", async (Vehicle v, AppDbContext db) =>
        {
            v.Name = v.Name.Trim();
            db.Vehicles.Add(v);
            await db.SaveChangesAsync();
            return Results.Created($"/api/vehicles/{v.Id}", v);
        });

        return app;
    }
}
