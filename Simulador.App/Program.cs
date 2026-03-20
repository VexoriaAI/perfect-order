using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Simulador.App.Auth;
using Simulador.App.Data;
using Simulador.App.Data.Seed;
using Simulador.App.Modules.BillingCompanies.Endpoints;
using Simulador.App.Modules.Catalog.Endpoints;
using Simulador.App.Modules.Config.Endpoints;
using Simulador.App.Modules.Customers.Endpoints;
using Simulador.App.Modules.Simulations;
using Simulador.App.Modules.Simulations.Endpoints;
using Simulador.App.Modules.Vehicles.Endpoints;
using Simulador.App.Shared;
using Simulador.Engine.Services;

var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();

// Forwarded Headers
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor |
        ForwardedHeaders.XForwardedProto;

    // libera proxies/rede conhecidos automaticamente
    options.KnownIPNetworks.Clear();
    options.KnownProxies.Clear();
});

// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;

    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;

    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/login";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
});

// Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Policies.AdminOnly, policy =>
        policy.RequireRole(Roles.Admin));
});

// Orchestrator + Engine
builder.Services.AddScoped<SimulationOrchestrator>();
builder.Services.AddScoped<SimulationEngine>();

// HttpClient para ApiClient (propaga cookies de auth)
var apiBaseUrl = builder.Configuration["App:BaseUrl"];

if (builder.Environment.IsDevelopment())
{
    apiBaseUrl ??= "http://localhost:5294/";
}
else if (string.IsNullOrWhiteSpace(apiBaseUrl))
{
    throw new InvalidOperationException("App:BaseUrl não configurado em produção.");
}

builder.Services.AddHttpClient<ApiClient>((sp, client) =>
{
    client.BaseAddress = new Uri(apiBaseUrl!);
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    UseCookies = false
});

var app = builder.Build();

// precisa vir bem cedo no pipeline
app.UseForwardedHeaders();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// só em dev, já que a plataforma termina HTTPS
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<Simulador.App.Components.App>()
    .AddInteractiveServerRenderMode();

// endpoints de auth
app.MapAuthEndpoints();

// endpoints atuais
app.MapCatalogEndpoints();
app.MapCustomersEndpoints();
app.MapVehiclesEndpoints();
app.MapConfigEndpoints();
app.MapSimulationEndpoints();
app.MapBillingCompanyEndpoints();

// seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await db.Database.MigrateAsync();

    await Seeder.SeedAsync(db);
    await RoleSeeder.SeedAsync(scope.ServiceProvider);
    await AdminSeeder.SeedAsync(scope.ServiceProvider, app.Configuration);
    await BillingCompanySeeder.SeedAsync(db);
}

app.Run();
