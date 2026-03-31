using Microsoft.EntityFrameworkCore;
using Simulador.App.Data;
using Simulador.App.Modules.Customers.Entities;

namespace Simulador.App.Data.Seed;

public static class CustomerSeederTop20FromExcel
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.Customers.AnyAsync())
            return;

        db.Customers.AddRange(new[]
        {
            new Customer
            {
                CustomerCode = "27",
                Name = "Acrelandia",
                IsActive = true,
                Uf = "AC",
                City = "Acrelandia",
                AllowedVehicles = new List<CustomerAllowedVehicle>
                {
                    new CustomerAllowedVehicle { VehicleName = "Bongo" },
                    new CustomerAllowedVehicle { VehicleName = "3/4" },
                    new CustomerAllowedVehicle { VehicleName = "Toco" },
                    new CustomerAllowedVehicle { VehicleName = "Truck" }
                },
                Rules = new CustomerRules
                {
                    AllowsBulk = true,
                    AllowsPalletized = true,
                    AllowsMix = true,
                    MaxKgPerPallet = 120m
                },
            },
            new Customer
            {
                CustomerCode = "411",
                Name = "Assis Brasil",
                IsActive = true,
                Uf = "AC",
                City = "Assis Brasil",
                AllowedVehicles = new List<CustomerAllowedVehicle>
                {
                    new CustomerAllowedVehicle { VehicleName = "Bongo" },
                    new CustomerAllowedVehicle { VehicleName = "3/4" },
                    new CustomerAllowedVehicle { VehicleName = "Toco" },
                    new CustomerAllowedVehicle { VehicleName = "Truck" }
                },
                Rules = new CustomerRules
                {
                    AllowsBulk = true,
                    AllowsPalletized = true,
                    AllowsMix = true,
                    MaxKgPerPallet = 120m
                },
            },
            new Customer
            {
                CustomerCode = "753",
                Name = "Brasileia",
                IsActive = true,
                Uf = "AC",
                City = "Brasileia",
                AllowedVehicles = new List<CustomerAllowedVehicle>
                {
                    new CustomerAllowedVehicle { VehicleName = "Bongo" },
                    new CustomerAllowedVehicle { VehicleName = "3/4" },
                    new CustomerAllowedVehicle { VehicleName = "Toco" },
                    new CustomerAllowedVehicle { VehicleName = "Truck" }
                },
                Rules = new CustomerRules
                {
                    AllowsBulk = true,
                    AllowsPalletized = true,
                    AllowsMix = true,
                    MaxKgPerPallet = 120m
                }
            },
            new Customer
            {
                CustomerCode = "796",
                Name = "Bujari",
                IsActive = true,
                Uf = "AC",
                City = "Bujari",
                AllowedVehicles = new List<CustomerAllowedVehicle>
                {
                    new CustomerAllowedVehicle { VehicleName = "Bongo" },
                    new CustomerAllowedVehicle { VehicleName = "3/4" },
                    new CustomerAllowedVehicle { VehicleName = "Toco" },
                    new CustomerAllowedVehicle { VehicleName = "Truck" }
                },
                Rules = new CustomerRules
                {
                    AllowsBulk = true,
                    AllowsPalletized = true,
                    AllowsMix = true,
                    MaxKgPerPallet = 120m
                }
            },
            new Customer
            {
                CustomerCode = "1083",
                Name = "Capixaba",
                IsActive = true,
                Uf = "AC",
                City = "Capixaba",
                AllowedVehicles = new List<CustomerAllowedVehicle>
                {
                    new CustomerAllowedVehicle { VehicleName = "Bongo" },
                    new CustomerAllowedVehicle { VehicleName = "3/4" },
                    new CustomerAllowedVehicle { VehicleName = "Toco" },
                    new CustomerAllowedVehicle { VehicleName = "Truck" }
                },
                Rules = new CustomerRules
                {
                    AllowsBulk = true,
                    AllowsPalletized = true,
                    AllowsMix = true,
                    MaxKgPerPallet = 120m
                }
            },
            new Customer
            {
                CustomerCode = "1484",
                Name = "Cruzeiro Do Sul",
                IsActive = true,
                Uf = "AC",
                City = "Cruzeiro Do Sul",
                AllowedVehicles = new List<CustomerAllowedVehicle>
                {
                    new CustomerAllowedVehicle { VehicleName = "Bongo" },
                    new CustomerAllowedVehicle { VehicleName = "3/4" },
                    new CustomerAllowedVehicle { VehicleName = "Toco" },
                    new CustomerAllowedVehicle { VehicleName = "Truck" }
                },
                Rules = new CustomerRules
                {
                    AllowsBulk = true,
                    AllowsPalletized = true,
                    AllowsMix = true,
                    MaxKgPerPallet = 120m
                }
            },
            new Customer
            {
                CustomerCode = "1679",
                Name = "Epitaciolandia",
                IsActive = true,
                Uf = "AC",
                City = "Epitaciolandia",
                AllowedVehicles = new List<CustomerAllowedVehicle>
                {
                    new CustomerAllowedVehicle { VehicleName = "Bongo" },
                    new CustomerAllowedVehicle { VehicleName = "3/4" },
                    new CustomerAllowedVehicle { VehicleName = "Toco" },
                    new CustomerAllowedVehicle { VehicleName = "Truck" }
                },
                Rules = new CustomerRules
                {
                    AllowsBulk = true,
                    AllowsPalletized = true,
                    AllowsMix = true,
                    MaxKgPerPallet = 120m
                }
            },
            new Customer
            {
                CustomerCode = "1757",
                Name = "Feijo",
                IsActive = true,
                Uf = "AC",
                City = "Feijo",
                AllowedVehicles = new List<CustomerAllowedVehicle>
                {
                    new CustomerAllowedVehicle { VehicleName = "Bongo" },
                    new CustomerAllowedVehicle { VehicleName = "3/4" },
                    new CustomerAllowedVehicle { VehicleName = "Toco" },
                    new CustomerAllowedVehicle { VehicleName = "Truck" }
                },
                Rules = new CustomerRules
                {
                    AllowsBulk = true,
                    AllowsPalletized = true,
                    AllowsMix = true,
                    MaxKgPerPallet = 120m
                }
            },
            new Customer
            {
                CustomerCode = "2608",
                Name = "Jordao",
                IsActive = true,
                Uf = "AC",
                City = "Jordao",
                AllowedVehicles = new List<CustomerAllowedVehicle>
                {
                    new CustomerAllowedVehicle { VehicleName = "Bongo" },
                    new CustomerAllowedVehicle { VehicleName = "3/4" },
                    new CustomerAllowedVehicle { VehicleName = "Toco" },
                    new CustomerAllowedVehicle { VehicleName = "Truck" }
                },
                Rules = new CustomerRules
                {
                    AllowsBulk = true,
                    AllowsPalletized = true,
                    AllowsMix = true,
                    MaxKgPerPallet = 120m
                }
            },
            new Customer
            {
                CustomerCode = "2886",
                Name = "Mancio Lima",
                IsActive = true,
                Uf = "AC",
                City = "Mancio Lima",
                AllowedVehicles = new List<CustomerAllowedVehicle>
                {
                    new CustomerAllowedVehicle { VehicleName = "Bongo" },
                    new CustomerAllowedVehicle { VehicleName = "3/4" },
                    new CustomerAllowedVehicle { VehicleName = "Toco" },
                    new CustomerAllowedVehicle { VehicleName = "Truck" }
                },
                Rules = new CustomerRules
                {
                    AllowsBulk = true,
                    AllowsPalletized = true,
                    AllowsMix = true,
                    MaxKgPerPallet = 120m
                }
            },
            new Customer
            {
                CustomerCode = "2900",
                Name = "Manoel Urbano",
                IsActive = true,
                Uf = "AC",
                City = "Manoel Urbano",
                AllowedVehicles = new List<CustomerAllowedVehicle>
                {
                    new CustomerAllowedVehicle { VehicleName = "Bongo" },
                    new CustomerAllowedVehicle { VehicleName = "3/4" },
                    new CustomerAllowedVehicle { VehicleName = "Toco" },
                    new CustomerAllowedVehicle { VehicleName = "Truck" }
                },
                Rules = new CustomerRules
                {
                    AllowsBulk = true,
                    AllowsPalletized = true,
                    AllowsMix = true,
                    MaxKgPerPallet = 120m
                }
            },
            new Customer
            {
                CustomerCode = "2946",
                Name = "Marechal Thaumaturgo",
                IsActive = true,
                Uf = "AC",
                City = "Marechal Thaumaturgo",
                AllowedVehicles = new List<CustomerAllowedVehicle>
                {
                    new CustomerAllowedVehicle { VehicleName = "Bongo" },
                    new CustomerAllowedVehicle { VehicleName = "3/4" },
                    new CustomerAllowedVehicle { VehicleName = "Toco" },
                    new CustomerAllowedVehicle { VehicleName = "Truck" }
                },
                Rules = new CustomerRules
                {
                    AllowsBulk = true,
                    AllowsPalletized = true,
                    AllowsMix = true,
                    MaxKgPerPallet = 120m
                }
            },
            new Customer
            {
                CustomerCode = "3873",
                Name = "Placido De Castro",
                IsActive = true,
                Uf = "AC",
                City = "Placido De Castro",
                AllowedVehicles = new List<CustomerAllowedVehicle>
                {
                    new CustomerAllowedVehicle { VehicleName = "Bongo" },
                    new CustomerAllowedVehicle { VehicleName = "3/4" },
                    new CustomerAllowedVehicle { VehicleName = "Toco" },
                    new CustomerAllowedVehicle { VehicleName = "Truck" }
                },
                Rules = new CustomerRules
                {
                    AllowsBulk = true,
                    AllowsPalletized = true,
                    AllowsMix = true,
                    MaxKgPerPallet = 120m
                }
            },
            new Customer
            {
                CustomerCode = "3946",
                Name = "Porto Acre",
                IsActive = true,
                Uf = "AC",
                City = "Porto Acre",
                AllowedVehicles = new List<CustomerAllowedVehicle>
                {
                    new CustomerAllowedVehicle { VehicleName = "Bongo" },
                    new CustomerAllowedVehicle { VehicleName = "3/4" },
                    new CustomerAllowedVehicle { VehicleName = "Toco" },
                    new CustomerAllowedVehicle { VehicleName = "Truck" }
                },
                Rules = new CustomerRules
                {
                    AllowsBulk = true,
                    AllowsPalletized = true,
                    AllowsMix = true,
                    MaxKgPerPallet = 120m
                }
            },
            new Customer
            {
                CustomerCode = "3980",
                Name = "Porto Walter",
                IsActive = true,
                Uf = "AC",
                City = "Porto Walter",
                AllowedVehicles = new List<CustomerAllowedVehicle>
                {
                    new CustomerAllowedVehicle { VehicleName = "Bongo" },
                    new CustomerAllowedVehicle { VehicleName = "3/4" },
                    new CustomerAllowedVehicle { VehicleName = "Toco" },
                    new CustomerAllowedVehicle { VehicleName = "Truck" }
                },
                Rules = new CustomerRules
                {
                    AllowsBulk = true,
                    AllowsPalletized = true,
                    AllowsMix = true,
                    MaxKgPerPallet = 120m
                }
            },
            new Customer
            {
                CustomerCode = "4193",
                Name = "Rio Branco",
                IsActive = true,
                Uf = "AC",
                City = "Rio Branco",
                AllowedVehicles = new List<CustomerAllowedVehicle>
                {
                    new CustomerAllowedVehicle { VehicleName = "Bongo" },
                    new CustomerAllowedVehicle { VehicleName = "3/4" },
                    new CustomerAllowedVehicle { VehicleName = "Toco" },
                    new CustomerAllowedVehicle { VehicleName = "Truck" }
                },
                Rules = new CustomerRules
                {
                    AllowsBulk = true,
                    AllowsPalletized = true,
                    AllowsMix = true,
                    MaxKgPerPallet = 120m
                }
            },
            new Customer
            {
                CustomerCode = "4262",
                Name = "Rodrigues Alves",
                IsActive = true,
                Uf = "AC",
                City = "Rodrigues Alves",
                AllowedVehicles = new List<CustomerAllowedVehicle>
                {
                    new CustomerAllowedVehicle { VehicleName = "Bongo" },
                    new CustomerAllowedVehicle { VehicleName = "3/4" },
                    new CustomerAllowedVehicle { VehicleName = "Toco" },
                    new CustomerAllowedVehicle { VehicleName = "Truck" }
                },
                Rules = new CustomerRules
                {
                    AllowsBulk = true,
                    AllowsPalletized = true,
                    AllowsMix = true,
                    MaxKgPerPallet = 120m
                }
            },
            new Customer
            {
                CustomerCode = "4474",
                Name = "Santa Rosa Do Purus",
                IsActive = true,
                Uf = "AC",
                City = "Santa Rosa Do Purus",
                AllowedVehicles = new List<CustomerAllowedVehicle>
                {
                    new CustomerAllowedVehicle { VehicleName = "Bongo" },
                    new CustomerAllowedVehicle { VehicleName = "3/4" },
                    new CustomerAllowedVehicle { VehicleName = "Toco" },
                    new CustomerAllowedVehicle { VehicleName = "Truck" }
                },
                Rules = new CustomerRules
                {
                    AllowsBulk = true,
                    AllowsPalletized = true,
                    AllowsMix = true,
                    MaxKgPerPallet = 120m
                }
            },
            new Customer
            {
                CustomerCode = "4963",
                Name = "Sena Madureira",
                IsActive = true,
                Uf = "AC",
                City = "Sena Madureira",
                AllowedVehicles = new List<CustomerAllowedVehicle>
                {
                    new CustomerAllowedVehicle { VehicleName = "Bongo" },
                    new CustomerAllowedVehicle { VehicleName = "3/4" },
                    new CustomerAllowedVehicle { VehicleName = "Toco" },
                    new CustomerAllowedVehicle { VehicleName = "Truck" }
                },
                Rules = new CustomerRules
                {
                    AllowsBulk = true,
                    AllowsPalletized = true,
                    AllowsMix = true,
                    MaxKgPerPallet = 120m
                }
            },
            new Customer
            {
                CustomerCode = "4971",
                Name = "Senador Guiomard",
                IsActive = true,
                Uf = "AC",
                City = "Senador Guiomard",
                AllowedVehicles = new List<CustomerAllowedVehicle>
                {
                    new CustomerAllowedVehicle { VehicleName = "Bongo" },
                    new CustomerAllowedVehicle { VehicleName = "3/4" },
                    new CustomerAllowedVehicle { VehicleName = "Toco" },
                    new CustomerAllowedVehicle { VehicleName = "Truck" }
                },
                Rules = new CustomerRules
                {
                    AllowsBulk = true,
                    AllowsPalletized = true,
                    AllowsMix = true,
                    MaxKgPerPallet = 120m
                }
            }
        });

        await db.SaveChangesAsync();
    }
}
