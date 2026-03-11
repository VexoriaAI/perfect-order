using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Simulador.App.Auth;
using Simulador.App.Data;
using Simulador.App.Data.Seed;
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

// seus serviços atuais
builder.Services.AddScoped<ApiClient>();
builder.Services.AddScoped<SimulationOrchestrator>();

// Engine
builder.Services.AddScoped<SimulationEngine>();
builder.Services.AddScoped<PalletCalculator>();
builder.Services.AddScoped<VehicleRecommender>();
builder.Services.AddScoped<FreightEstimator>();

// se já tiver HttpClient registrado, deixa
builder.Services.AddHttpClient<ApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["App:BaseUrl"] ?? "http://localhost:5294/");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
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

// seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await db.Database.MigrateAsync();

    await RoleSeeder.SeedAsync(scope.ServiceProvider);
    await AdminSeeder.SeedAsync(scope.ServiceProvider, app.Configuration);
}

app.Run();