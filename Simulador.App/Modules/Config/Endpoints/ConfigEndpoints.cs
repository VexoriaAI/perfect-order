using Microsoft.EntityFrameworkCore;
using Simulador.App.Data;
using Simulador.App.Modules.Config.Entities;

namespace Simulador.App.Modules.Config.Endpoints;

public static class ConfigEndpoints
{
    public static IEndpointRouteBuilder MapConfigEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/config").WithTags("Config")
            .RequireAuthorization();

        group.MapGet("/", async (AppDbContext db) =>
            await db.SystemConfigs.AsNoTracking().FirstAsync(x => x.Key == "DEFAULT"));

        group.MapPut("/", async (SystemConfig cfg, AppDbContext db) =>
        {
            var current = await db.SystemConfigs.FirstAsync(x => x.Key == "DEFAULT");
            current.DefaultLossPercent = cfg.DefaultLossPercent;
            current.FreightIdealPercent = cfg.FreightIdealPercent;
            current.FreightAcceptablePercent = cfg.FreightAcceptablePercent;
            current.MissingPricePolicy = cfg.MissingPricePolicy;
            await db.SaveChangesAsync();
            return Results.Ok(current);
        });

        return app;
    }
}
