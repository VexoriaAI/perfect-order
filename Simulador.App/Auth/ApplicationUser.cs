using Microsoft.AspNetCore.Identity;

namespace Simulador.App.Auth;

public sealed class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}