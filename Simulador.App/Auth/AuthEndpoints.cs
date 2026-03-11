using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Simulador.App.Auth;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth").WithTags("Auth");

        group.MapPost("/login", async (
            LoginRequest request,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager) =>
        {
            var email = request.Email.Trim().ToLowerInvariant();

            var user = await userManager.FindByEmailAsync(email);
            if (user is null)
                return Results.BadRequest(new { error = "E-mail ou senha inválidos." });

            var result = await signInManager.PasswordSignInAsync(
                user.UserName!,
                request.Password,
                request.RememberMe,
                lockoutOnFailure: false);

            if (!result.Succeeded)
                return Results.BadRequest(new { error = "E-mail ou senha inválidos." });

            return Results.Ok(new
            {
                ok = true,
                name = user.FullName,
                email = user.Email
            });
        }).AllowAnonymous();

        group.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager) =>
        {
            await signInManager.SignOutAsync();
            return Results.Ok(new { ok = true });
        }).RequireAuthorization();

        group.MapGet("/me", (ClaimsPrincipal user) =>
        {
            return Results.Ok(new
            {
                isAuthenticated = user.Identity?.IsAuthenticated ?? false,
                name = user.Identity?.Name,
                email = user.FindFirstValue(ClaimTypes.Email),
                roles = user.FindAll(ClaimTypes.Role).Select(x => x.Value).ToArray()
            });
        }).RequireAuthorization();

        return app;
    }
}

public sealed class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool RememberMe { get; set; }
}