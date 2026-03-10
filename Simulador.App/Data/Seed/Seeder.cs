using Microsoft.EntityFrameworkCore;
using Simulador.App.Modules.Config.Entities;
using Simulador.App.Modules.Vehicles.Entities;
using Simulador.App.Modules.Customers.Entities;
using Simulador.App.Modules.Catalog.Entities;

namespace Simulador.App.Data.Seed;

public static class Seeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        // Config
        if (!await db.SystemConfigs.AnyAsync())
        {
            db.SystemConfigs.Add(new SystemConfig
            {
                Key = "DEFAULT",
                DefaultLossPercent = 0.15m,
                FreightIdealPercent = 0.06m,
                FreightAcceptablePercent = 0.08m,
                MissingPricePolicy = 0
            });
        }

        // Vehicles (ordem do menor para o maior)
        if (!await db.Vehicles.AnyAsync())
        {
            db.Vehicles.AddRange(new[]
            {
                new Vehicle { Name="Fiorino", MaxVolumeM3=2m,   MaxWeightKg=500m,  MaxPallets=0,  OrderIndex=1 },
                new Vehicle { Name="Bongo",   MaxVolumeM3=12m,  MaxWeightKg=1500m, MaxPallets=0,  OrderIndex=2 },
                new Vehicle { Name="3/4",     MaxVolumeM3=32m,  MaxWeightKg=3500m, MaxPallets=6,  OrderIndex=3 },
                new Vehicle { Name="Toco",    MaxVolumeM3=40m,  MaxWeightKg=6000m, MaxPallets=10, OrderIndex=4 },
                new Vehicle { Name="Truck",   MaxVolumeM3=50m,  MaxWeightKg=16000m,MaxPallets=14, OrderIndex=5 },
                new Vehicle { Name="Carreta Leve",   MaxVolumeM3=100m, MaxWeightKg=25000m, MaxPallets=28, OrderIndex=6 },
                new Vehicle { Name="Carreta Pesada", MaxVolumeM3=100m, MaxWeightKg=31600m, MaxPallets=28, OrderIndex=7 },
            });
        }

        // Opcional: dados mínimos pra você testar imediatamente
        if (!await db.Customers.AnyAsync())
        {
            var cust = new Customer
            {
                CustomerCode = "C001",
                Name = "Cliente Teste",
                IsActive = true,
                Uf = "SP",
                City = "Sao Paulo",
                Rules = new CustomerRules
                {
                    AllowsBulk = true,
                    AllowsPalletized = true,
                    AllowsMix = true,
                    MaxKgPerPallet = 120m
                },
                AllowedVehicles = new List<CustomerAllowedVehicle>
                {
                    new CustomerAllowedVehicle { VehicleName = "Bongo" },
                    new CustomerAllowedVehicle { VehicleName = "3/4" },
                    new CustomerAllowedVehicle { VehicleName = "Toco" },
                    new CustomerAllowedVehicle { VehicleName = "Truck" }
                }
            };
            db.Customers.Add(cust);
        }

        if (!await db.Products.AnyAsync())
        {
            db.Products.AddRange(new[]
            {
                new Product { Sku="SKU001", Description="Produto 1", Unit="UN", WeightKg=2m, VolumeM3=0.03m, PalletUnitsDefault=30, PackagingType="CX" },
                new Product { Sku="SKU002", Description="Produto 2", Unit="UN", WeightKg=1m, VolumeM3=0.02m, PalletUnitsDefault=50, PackagingType="FD" }
            });
            db.SkuRules.AddRange(new[]
            {
                new SkuRule { Sku="SKU001", IsValidForSale=true, IsInactive=false },
                new SkuRule { Sku="SKU002", IsValidForSale=true, IsInactive=false }
            });
        }

        await db.SaveChangesAsync();
    }
}