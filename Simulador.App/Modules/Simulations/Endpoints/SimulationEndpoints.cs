using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Simulador.App.Modules.Simulations;
using Simulador.Engine.Contracts;

namespace Simulador.App.Modules.Simulations.Endpoints;

public static class SimulationEndpoints
{
    public static IEndpointRouteBuilder MapSimulationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/simulations")
            .WithTags("Simulations")
            .RequireAuthorization();

        group.MapPost("/", async (SimulationRequest req, SimulationOrchestrator orchestrator) =>
        {
            if (req.Items is null || req.Items.Count == 0)
                return Results.BadRequest(new { error = "Items required" });

            foreach (var i in req.Items)
            {
                if (string.IsNullOrWhiteSpace(i.Sku))
                    return Results.BadRequest(new { error = "SKU required" });

                if (i.Quantity <= 0)
                    return Results.BadRequest(new { error = $"Quantity must be > 0 (sku={i.Sku})" });

                if (req.IsSeller && i.UnitPrice is null)
                    return Results.BadRequest(new { error = $"UnitPrice required for seller (sku={i.Sku})" });
            }

            try
            {
                var (result, runId) = await orchestrator.RunAndPersistAsync(req);
                return Results.Ok(new { runId, result });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        });

        return app;
    }
}
