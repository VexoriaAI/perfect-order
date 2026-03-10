using Microsoft.EntityFrameworkCore;
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

// Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Orchestrator + Engine
builder.Services.AddScoped<SimulationOrchestrator>();
builder.Services.AddSingleton<SimulationEngine>();

builder.Services.AddScoped<Simulador.App.Shared.AppState>();
builder.Services.AddScoped<Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage.ProtectedSessionStorage>();

builder.Services.AddScoped<AppState>();

builder.Services.AddHttpClient<ApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["App:BaseUrl"] ?? "http://localhost:5294/");
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapCatalogEndpoints();
app.MapCustomersEndpoints();
app.MapVehiclesEndpoints();
app.MapConfigEndpoints();
app.MapSimulationEndpoints();

// Blazor
app.MapRazorComponents<Simulador.App.Components.App>()
   .AddInteractiveServerRenderMode();

// Auto-migrate + seed (DEV)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
    await Seeder.SeedAsync(db);
}

app.Run();