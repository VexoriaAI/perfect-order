using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Simulador.App.Data;
using Simulador.App.Modules.Simulations;
using Simulador.App.Modules.Simulations.Dtos;
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
        
        group.MapGet("/history", async (
            AppDbContext db) =>
        {
            var result = await db.SimulationRuns
                .AsNoTracking()
                .Include(x => x.Items)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new SimulationHistoryRowDto
                {
                    RunId = x.Id,
                    CreatedAt = x.CreatedAt,
                    CompanyBilling = x.CompanyBilling,
                    IsSeller = x.IsSeller,
                    SellerId = x.SellerId,
                    RegionalId = x.RegionalId,
                    StateCode = x.StateCode,
                    CityName = x.CityName,
                    ItemCount = x.Items.Count
                })
                .ToListAsync();

            return Results.Ok(result);
        });

        group.MapGet("/{runId:guid}/history-detail", async (
            Guid runId,
            AppDbContext db) =>
        {
            var run = await db.SimulationRuns
                .AsNoTracking()
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == runId);

            if (run is null)
                return Results.NotFound();

            var dto = new SimulationHistoryDetailDto
            {
                RunId = run.Id,
                CreatedAt = run.CreatedAt,
                CompanyBilling = run.CompanyBilling,
                IsSeller = run.IsSeller,
                SellerId = run.SellerId,
                RegionalId = run.RegionalId,
                StateCode = run.StateCode,
                CityName = run.CityName,
                RequestJson = run.RequestJson,
                ResultJson = run.ResultJson,
                Items = run.Items.Select(i => new SimulationHistoryItemDto
                {
                    InternalCode = i.InternalCode,
                    Sku = i.Sku,
                    Description = i.Description,
                    Quantity = i.Quantity,
                    UnitPriceInput = i.UnitPriceInput
                }).ToList()
            };

            return Results.Ok(dto);
        });

        return app;
    }
}