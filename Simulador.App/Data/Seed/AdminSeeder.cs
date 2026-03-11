using Microsoft.AspNetCore.Identity;
using Simulador.App.Auth;

namespace Simulador.App.Data.Seed;

public static class AdminSeeder
{
    public static async Task SeedAsync(IServiceProvider services, IConfiguration configuration)
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        var section = configuration.GetSection("SeedAdmin");

        var email = section["Email"]?.Trim().ToLowerInvariant();
        var password = section["Password"];
        var fullName = section["FullName"] ?? "Administrador";

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            return;

        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
        {
            user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = fullName,
                EmailConfirmed = true
            };

            var createResult = await userManager.CreateAsync(user, password);
            if (!createResult.Succeeded)
            {
                var errors = string.Join(" | ", createResult.Errors.Select(x => x.Description));
                throw new InvalidOperationException($"Falha ao criar admin seed: {errors}");
            }
        }
        else
        {
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var resetResult = await userManager.ResetPasswordAsync(user, token, password);

            if (!resetResult.Succeeded)
            {
                var errors = string.Join(" | ", resetResult.Errors.Select(x => x.Description));
                throw new InvalidOperationException($"Falha ao resetar senha do admin seed: {errors}");
            }

            user.FullName = fullName;
            user.EmailConfirmed = true;
            await userManager.UpdateAsync(user);
        }

        if (!await userManager.IsInRoleAsync(user, Roles.Admin))
        {
            var roleResult = await userManager.AddToRoleAsync(user, Roles.Admin);
            if (!roleResult.Succeeded)
            {
                var errors = string.Join(" | ", roleResult.Errors.Select(x => x.Description));
                throw new InvalidOperationException($"Falha ao adicionar role Admin: {errors}");
            }
        }
    }
}