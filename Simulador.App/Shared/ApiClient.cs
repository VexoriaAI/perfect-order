using System.Net.Http.Json;
using Simulador.Engine.Contracts;

namespace Simulador.App.Shared;

public sealed class ApiClient
{
    private readonly HttpClient _http;
    private readonly IHttpContextAccessor _ctx;

    public ApiClient(HttpClient http, IHttpContextAccessor ctx)
    {
        _http = http;
        _ctx = ctx;
    }

    private void PropagateAuth()
    {
        var cookieHeader = _ctx.HttpContext?.Request.Headers.Cookie.ToString();
        if (!string.IsNullOrWhiteSpace(cookieHeader))
        {
            _http.DefaultRequestHeaders.Remove("Cookie");
            _http.DefaultRequestHeaders.Add("Cookie", cookieHeader);
        }
    }

    public async Task<List<CustomerMini>> GetCustomersAsync()
    {
        PropagateAuth();
        var res = await _http.GetFromJsonAsync<List<CustomerMini>>("/api/customers");
        return res ?? new();
    }

    public async Task<SystemConfigDto?> GetConfigAsync()
    {
        PropagateAuth();
        return await _http.GetFromJsonAsync<SystemConfigDto>("/api/config");
    }

    public async Task<SimulationApiResponse?> SimulateAsync(SimulationRequest req)
    {
        PropagateAuth();
        var resp = await _http.PostAsJsonAsync("/api/simulations", req);
        if (!resp.IsSuccessStatusCode)
        {
            var err = await resp.Content.ReadAsStringAsync();
            throw new Exception(err);
        }
        return await resp.Content.ReadFromJsonAsync<SimulationApiResponse>();
    }
}

// DTOs mínimos só pra UI
public sealed class CustomerMini
{
    public string CustomerCode { get; set; } = default!;
    public string Name { get; set; } = default!;
    public bool IsActive { get; set; }
}

public sealed class SystemConfigDto
{
    public string Key { get; set; } = "DEFAULT";
    public decimal DefaultLossPercent { get; set; }
    public decimal FreightIdealPercent { get; set; }
    public decimal FreightAcceptablePercent { get; set; }
    public int MissingPricePolicy { get; set; }
}

public sealed class SimulationApiResponse
{
    public Guid RunId { get; set; }
    public SimulationResult Result { get; set; } = new();
}
