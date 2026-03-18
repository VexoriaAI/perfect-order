using Microsoft.EntityFrameworkCore;
using Simulador.App.Data;
using Simulador.App.Modules.BillingCompanies.Entities;

namespace Simulador.App.Data.Seed;

public static class BillingCompanySeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.BillingCompanies.AnyAsync())
            return;

        var items = new[]
        {
            new BillingCompany
            {
                Id = Guid.Parse("0bbe70ba-0aea-51ff-b1f5-fb828345c648"),
                CompanyCode = "1",
                Name = "Limppano S A - Matriz",
                DisplayName = "1 - Limppano S A - Matriz",
                IsActive = true
            },
            new BillingCompany
            {
                Id = Guid.Parse("e5547691-f7c0-58ab-a391-c0958dce6732"),
                CompanyCode = "2",
                Name = "Ava SP - Matriz",
                DisplayName = "2 - Ava SP - Matriz",
                IsActive = true
            },
            new BillingCompany
            {
                Id = Guid.Parse("55e4a51e-a2b9-53cc-935a-f11917d2fdf4"),
                CompanyCode = "3",
                Name = "Burn Industria",
                DisplayName = "3 - Burn Industria",
                IsActive = true
            },
            new BillingCompany
            {
                Id = Guid.Parse("00d3018a-e3a6-5c58-b9bb-c04191e61b3e"),
                CompanyCode = "4",
                Name = "Van Distribuidora",
                DisplayName = "4 - Van Distribuidora",
                IsActive = true
            },
            new BillingCompany
            {
                Id = Guid.Parse("62348d98-6778-531c-8c7c-5c2c6b55d6f4"),
                CompanyCode = "5",
                Name = "Ava Rio de Janeiro",
                DisplayName = "5 - Ava Rio de Janeiro",
                IsActive = true
            },
            new BillingCompany
            {
                Id = Guid.Parse("269cfee5-2def-5141-9c6f-c625de712388"),
                CompanyCode = "6",
                Name = "Limppano - Filial Rs",
                DisplayName = "6 - Limppano - Filial Rs",
                IsActive = true
            },
            new BillingCompany
            {
                Id = Guid.Parse("dd888d55-854b-5f7d-b730-287fdc2f069f"),
                CompanyCode = "9",
                Name = "Ava SP - Filial",
                DisplayName = "9 - Ava SP - Filial",
                IsActive = true
            }
        };

        db.BillingCompanies.AddRange(items);
        await db.SaveChangesAsync();
    }
}