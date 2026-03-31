using Microsoft.EntityFrameworkCore;
using Simulador.App.Data;
using Simulador.App.Modules.Regional.DTO;
using RegionalEntity = Simulador.App.Modules.Regional.Entities.Regional;

namespace Simulador.App.Modules.Regional.Endpoints;

public static class RegionalEndpoints
{
    public static IEndpointRouteBuilder MapRegionalEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/lookups/regionals", async (
            string? sellerUserId,
            AppDbContext db) =>
        {
            IQueryable<RegionalEntity> query = db.Regionals
                .AsNoTracking()
                .Where(x => x.IsActive);

            if (!string.IsNullOrWhiteSpace(sellerUserId))
            {
                var regionalIds = await db.SellerRegionals
                    .AsNoTracking()
                    .Where(x => x.SellerUserId == sellerUserId)
                    .Select(x => x.RegionalId)
                    .ToListAsync();

                query = query.Where(x => regionalIds.Contains(x.Id));
            }

            var result = await query
                .OrderBy(x => x.Name)
                .Select(x => new RegionalMini
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name
                })
                .ToListAsync();

            return Results.Ok(result);
        }).RequireAuthorization();

        return app;
    }
}