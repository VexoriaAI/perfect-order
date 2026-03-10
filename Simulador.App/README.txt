Simulador.sln
└─ src
   ├─ Simulador.App/                     // Blazor Web App + API + EF + Identity
   │  ├─ Program.cs
   │  ├─ appsettings.json
   │  ├─ Data/
   │  │  ├─ AppDbContext.cs
   │  │  ├─ Migrations/
   │  │  └─ Seed/
   │  │     ├─ RoleSeeder.cs
   │  │     └─ AdminSeeder.cs
   │  ├─ Auth/
   │  │  ├─ ApplicationUser.cs
   │  │  ├─ JwtOptions.cs
   │  │  ├─ TokenService.cs
   │  │  └─ Policies.cs
   │  ├─ Modules/
   │  │  ├─ Catalog/
   │  │  │  ├─ Entities/                 // Product, SkuRule, PriceAverage
   │  │  │  ├─ Endpoints/                // minimal api endpoints
   │  │  │  └─ Dtos/
   │  │  ├─ Customers/
   │  │  │  ├─ Entities/                 // Customer, CustomerRules, AllowedVehicles
   │  │  │  ├─ Endpoints/
   │  │  │  └─ Dtos/
   │  │  ├─ Vehicles/
   │  │  │  ├─ Entities/                 // Vehicle
   │  │  │  ├─ Endpoints/
   │  │  │  └─ Dtos/
   │  │  ├─ Config/
   │  │  │  ├─ Entities/                 // SystemConfig
   │  │  │  ├─ Endpoints/
   │  │  │  └─ Dtos/
   │  │  └─ Simulations/
   │  │     ├─ Entities/                 // SimulationRun, SimulationItem
   │  │     ├─ Endpoints/
   │  │     ├─ Dtos/
   │  │     └─ SimulationOrchestrator.cs // carrega snapshots e chama Engine
   │  ├─ Components/
   │  │  ├─ Layout/                      // NavMenu etc.
   │  │  └─ Pages/
   │  │     ├─ Home.razor
   │  │     ├─ Login.razor
   │  │     ├─ Simulator.razor
   │  │     ├─ Admin/
   │  │     │  ├─ Products.razor
   │  │     │  ├─ Customers.razor
   │  │     │  ├─ Vehicles.razor
   │  │     │  └─ Config.razor
   │  │     └─ SimulationsHistory.razor
   │  ├─ Shared/
   │  │  ├─ ApiClient.cs                 // chama endpoints internos (HttpClient)
   │  │  └─ UiModels.cs
   │  └─ wwwroot/
   │
   ├─ Simulador.Engine/                  // Motor puro (sem EF/DB)
   │  ├─ Contracts/
   │  │  ├─ SimulationRequest.cs
   │  │  ├─ SimulationResult.cs
   │  │  └─ Snapshots.cs                 // ProductSnapshot, CustomerRulesSnapshot etc.
   │  ├─ Services/
   │  │  ├─ SimulationEngine.cs
   │  │  ├─ PalletCalculator.cs
   │  │  ├─ VehicleRecommender.cs
   │  │  └─ FreightEstimator.cs
   │  └─ Validation/
   │     └─ SimulationRequestValidator.cs
   │
   └─ Simulador.Tests/
      ├─ Engine.Tests/                   // testes do motor (principal)
      └─ App.Tests/                      // (opcional) integration tests da API