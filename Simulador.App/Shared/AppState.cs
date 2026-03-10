using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Simulador.App.Shared;

public enum AppRole { None, Vendedor, Admin }

public sealed class AppState
{
    private const string Key = "mvp_session";
    private readonly ProtectedSessionStorage _session;

    public AppState(ProtectedSessionStorage session) => _session = session;

    public AppRole Role { get; private set; } = AppRole.None;
    public string? DisplayName { get; private set; }
    public bool IsLoggedIn => Role != AppRole.None;
    public bool IsAdmin => Role == AppRole.Admin;
    public bool IsSeller => Role == AppRole.Vendedor;

    public async Task LoadAsync()
    {
        var result = await _session.GetAsync<SessionDto>(Key);
        if (result.Success && result.Value is not null)
        {
            Role = result.Value.Role;
            DisplayName = result.Value.DisplayName;
        }
    }

    public async Task LoginAsync(AppRole role, string? displayName)
    {
        Role = role;
        DisplayName = string.IsNullOrWhiteSpace(displayName) ? null : displayName.Trim();
        await _session.SetAsync(Key, new SessionDto { Role = Role, DisplayName = DisplayName });
    }

    public async Task LogoutAsync()
    {
        Role = AppRole.None;
        DisplayName = null;
        await _session.DeleteAsync(Key);
    }

    private sealed class SessionDto
    {
        public AppRole Role { get; set; }
        public string? DisplayName { get; set; }
    }
}