using Microsoft.AspNetCore.Identity;
using Simulador.App.Auth;

namespace Simulador.App.Modules.Sellers.Endpoints;

public static class SellerEndpoints
{
    public static IEndpointRouteBuilder MapSellerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/lookups")
            .WithTags("Sellers")
            .RequireAuthorization();

        group.MapGet("/sellers", async (UserManager<ApplicationUser> userManager) =>
        {
            var sellers = await userManager.GetUsersInRoleAsync(Roles.Vendedor);

            var result = sellers
                .OrderBy(x => x.FullName ?? x.Email)
                .Select(x => new SellerMini
                {
                    Id = x.Id,
                    Name = string.IsNullOrWhiteSpace(x.FullName) ? x.Email! : x.FullName,
                    Email = x.Email!
                })
                .ToList();

            return Results.Ok(result);
        });

        return app;
    }
}