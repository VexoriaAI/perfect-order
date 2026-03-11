using Microsoft.AspNetCore.Identity;
using Simulador.App.Auth;

namespace Simulador.App.Data.Seed;

public static class RoleSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        var roles = new[] { Roles.Admin, Roles.Vendedor };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(role));
                if (!result.Succeeded)
                {
                    var errors = string.Join(" | ", result.Errors.Select(x => x.Description));
                    throw new InvalidOperationException($"Falha ao criar role '{role}': {errors}");
                }
            }
        }
    }
}