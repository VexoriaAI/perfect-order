using System.Net.Http.Json;
using System.Text.Json;
using Simulador.App.Modules.BillingCompanies.DTOs;
using Simulador.App.Modules.Regional.DTO;
using Simulador.App.Modules.Simulations.Dtos;
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

    public async Task<List<BillingCompanyMini>> GetBillingCompaniesAsync()
    {
        PropagateAuth();
        var result = await _http.GetFromJsonAsync<List<BillingCompanyMini>>("/api/billing-companies");
        return result ?? new();
    }

    public async Task<List<ProductMini>> GetProductsAsync()
    {
        PropagateAuth();

        var resp = await _http.GetAsync("/api/products/lookup");
        var body = await resp.Content.ReadAsStringAsync();

        if (!resp.IsSuccessStatusCode)
            throw new Exception($"Erro ao buscar produtos: {(int)resp.StatusCode} {resp.ReasonPhrase}. Detalhe: {body}");

        var result = JsonSerializer.Deserialize<List<ProductMini>>(
            body,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        return result ?? new();
    }

    public async Task<List<SellerMini>> GetSellersAsync()
    {
        PropagateAuth();

        var resp = await _http.GetAsync("/api/lookups/sellers");
        var body = await resp.Content.ReadAsStringAsync();

        if (!resp.IsSuccessStatusCode)
            throw new Exception($"Erro ao buscar vendedores: {(int)resp.StatusCode} {resp.ReasonPhrase}. Detalhe: {body}");

        var result = System.Text.Json.JsonSerializer.Deserialize<List<SellerMini>>(
            body,
            new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        return result ?? new();
    }

    public async Task<List<RegionalMini>> GetRegionalsAsync(string? sellerUserId)
    {
        PropagateAuth();

        var url = string.IsNullOrWhiteSpace(sellerUserId)
            ? "/api/lookups/regionals"
            : $"/api/lookups/regionals?sellerUserId={Uri.EscapeDataString(sellerUserId)}";

        var result = await _http.GetFromJsonAsync<List<RegionalMini>>(url);
        return result ?? new();
    }

    public async Task<List<SimulationHistoryRowDto>> GetSimulationHistoryAsync()
    {
        PropagateAuth();
        var result = await _http.GetFromJsonAsync<List<SimulationHistoryRowDto>>("/api/simulations/history");
        return result ?? new();
    }

    public async Task<SimulationHistoryDetailDto?> GetSimulationHistoryDetailAsync(Guid runId)
    {
        PropagateAuth();
        return await _http.GetFromJsonAsync<SimulationHistoryDetailDto>($"/api/simulations/{runId}/history-detail");
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

public sealed class ProductMini
{
    public Guid Id { get; set; }
    public string InternalCode { get; set; } = default!;
    public string Sku { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool IsActive { get; set; }

    public string DisplayLabel => $"{InternalCode} - {Description}";
}