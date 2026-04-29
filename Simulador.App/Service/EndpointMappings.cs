using Simulador.App.Modules.Catalog.Endpoints;
using Simulador.App.Modules.Config.Endpoints;
using Simulador.App.Modules.Customers.Endpoints;
using Simulador.App.Modules.Sellers.Endpoints;
using Simulador.App.Modules.Regional.Endpoints;
using Simulador.App.Modules.Simulations.Endpoints;
using Simulador.App.Modules.BillingCompanies.Endpoints;
using Simulador.App.Modules.Vehicles.Endpoints;

namespace Simulador.App;

public static class EndpointMappings
{
    public static IEndpointRouteBuilder MapAppEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCatalogEndpoints();
        app.MapConfigEndpoints();
        app.MapCustomersEndpoints();
        app.MapSellerEndpoints();
        app.MapRegionalEndpoints();
        app.MapSimulationEndpoints();
        app.MapBillingCompanyEndpoints();
        app.MapVehiclesEndpoints();

        return app;
    }
}