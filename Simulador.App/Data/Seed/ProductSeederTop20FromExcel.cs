using Microsoft.EntityFrameworkCore;
using Simulador.App.Data;
using Simulador.App.Modules.Catalog.Entities;

namespace Simulador.App.Data.Seed;

public static class ProductSeederTop20FromExcel
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (!await db.Products.AnyAsync())
        {
            db.Products.AddRange(new[]
            {
            new Product
            {
                InternalCode = "12025",
                Sku = "12025",
                Description = "ESFREGAO EMBALAGEM ECONOMICA 8X6X3",
                Unit = "FD",
                PackagingType = "FD",
                VolumeM3 = 0.0997m,
                WeightKg = 9.35m,
                PalletUnitsDefault = 20,
                IsActive = true,
                DefaultUnitPrice = null
            },
            new Product
            {
                InternalCode = "12026",
                Sku = "12026",
                Description = "ESFREGAO FELICITY EMB.ECONOMICA 8X6X3",
                Unit = "FD",
                PackagingType = "FD",
                VolumeM3 = 0.0997m,
                WeightKg = 9.35m,
                PalletUnitsDefault = 20,
                IsActive = true,
                DefaultUnitPrice = null
            },
            new Product
            {
                InternalCode = "12027",
                Sku = "12027",
                Description = "ESFREGAO EMBALAGEM ECONOMICA 8X6X3",
                Unit = "FD",
                PackagingType = "FD",
                VolumeM3 = 0.0997m,
                WeightKg = 9.35m,
                PalletUnitsDefault = 20,
                IsActive = false,
                DefaultUnitPrice = null
            },
            new Product
            {
                InternalCode = "12028",
                Sku = "12028",
                Description = "ESFREGAO FELICITY EMB.ECONOMICA 8X6X3",
                Unit = "FD",
                PackagingType = "FD",
                VolumeM3 = 0.0997m,
                WeightKg = 9.35m,
                PalletUnitsDefault = 20,
                IsActive = false,
                DefaultUnitPrice = null
            },
            new Product
            {
                InternalCode = "12037",
                Sku = "12037",
                Description = "ESFREGAO WISH SORTIDO EMB. ECON.8X6X3",
                Unit = "FD",
                PackagingType = "FD",
                VolumeM3 = 0.0997m,
                WeightKg = 9.35m,
                PalletUnitsDefault = 20,
                IsActive = false,
                DefaultUnitPrice = null
            },
            new Product
            {
                InternalCode = "12950",
                Sku = "12950",
                Description = "CABOS DE VASSOURA",
                Unit = "FD",
                PackagingType = "FD",
                VolumeM3 = 0.00378m,
                WeightKg = 9.625m,
                PalletUnitsDefault = 21,
                IsActive = false,
                DefaultUnitPrice = null
            },
            new Product
            {
                InternalCode = "20102",
                Sku = "20102",
                Description = "FLANELA FLANEX LIMPPANO PEQ SUPERF 48X1",
                Unit = "FD",
                PackagingType = "FD",
                VolumeM3 = 0.0073m,
                WeightKg = 1.02m,
                PalletUnitsDefault = 150,
                IsActive = false,
                DefaultUnitPrice = null
            },
            new Product
            {
                InternalCode = "20106",
                Sku = "20106",
                Description = "PANO EXPERT ELETRONICO 15X1",
                Unit = "CX",
                PackagingType = "CX",
                VolumeM3 = 0.0063m,
                WeightKg = 0.52m,
                PalletUnitsDefault = 160,
                IsActive = false,
                DefaultUnitPrice = null
            },
            new Product
            {
                InternalCode = "20109",
                Sku = "20109",
                Description = "PANO EXPERT MULTIUSO 15X2",
                Unit = "CX",
                PackagingType = "CX",
                VolumeM3 = 0.0136m,
                WeightKg = 1.235m,
                PalletUnitsDefault = 104,
                IsActive = true,
                DefaultUnitPrice = null
            },
            new Product
            {
                InternalCode = "20202",
                Sku = "20202",
                Description = "FLANELA FLANEX LIMPPANO MED SUPERF 48X1",
                Unit = "FD",
                PackagingType = "FD",
                VolumeM3 = 0.0085m,
                WeightKg = 1.28m,
                PalletUnitsDefault = 150,
                IsActive = false,
                DefaultUnitPrice = null
            },
            new Product
            {
                InternalCode = "20221",
                Sku = "20221",
                Description = "PEDRA PERFUMADA LAVANDA PROMO 24X1",
                Unit = "CX",
                PackagingType = "CX",
                VolumeM3 = 0.0054m,
                WeightKg = 0.84m,
                PalletUnitsDefault = 270,
                IsActive = false,
                DefaultUnitPrice = null
            },
            new Product
            {
                InternalCode = "20223",
                Sku = "20223",
                Description = "PEDRA PERFUMADA PINHO PROMO 24X1",
                Unit = "CX",
                PackagingType = "CX",
                VolumeM3 = 0.0054m,
                WeightKg = 0.84m,
                PalletUnitsDefault = 270,
                IsActive = false,
                DefaultUnitPrice = null
            },
            new Product
            {
                InternalCode = "20230",
                Sku = "20230",
                Description = "PEDRA PERFUMADA LAVANDA 8X1",
                Unit = "CX",
                PackagingType = "CX",
                VolumeM3 = 0.00141075m,
                WeightKg = 0.295m,
                PalletUnitsDefault = 270,
                IsActive = false,
                DefaultUnitPrice = null
            },
            new Product
            {
                InternalCode = "20231",
                Sku = "20231",
                Description = "ODD PEDRA PERFUMADA LAVANDA 24X1",
                Unit = "CX",
                PackagingType = "CX",
                VolumeM3 = 0.0054m,
                WeightKg = 0.895m,
                PalletUnitsDefault = 270,
                IsActive = true,
                DefaultUnitPrice = null
            },
            new Product
            {
                InternalCode = "20232",
                Sku = "20232",
                Description = "PEDRA PERFUMADA PINHO 8X1",
                Unit = "CX",
                PackagingType = "CX",
                VolumeM3 = 0.00141075m,
                WeightKg = 0.295m,
                PalletUnitsDefault = 0,
                IsActive = false,
                DefaultUnitPrice = null
            },
            new Product
            {
                InternalCode = "20233",
                Sku = "20233",
                Description = "PLG WISH AZUL 30X3",
                Unit = "CX",
                PackagingType = "CX",
                VolumeM3 = 0.0132m,
                WeightKg = 1.05m,
                PalletUnitsDefault = 130,
                IsActive = false,
                DefaultUnitPrice = null
            },
            new Product
            {
                InternalCode = "20234",
                Sku = "20234",
                Description = "PLG WISH ROSA 30X3",
                Unit = "CX",
                PackagingType = "CX",
                VolumeM3 = 0.00141075m,
                WeightKg = 1.05m,
                PalletUnitsDefault = 130,
                IsActive = false,
                DefaultUnitPrice = null
            },
            new Product
            {
                InternalCode = "20235",
                Sku = "20235",
                Description = "ODD PEDRA PERFUMADA PINHO 24X1",
                Unit = "CX",
                PackagingType = "CX",
                VolumeM3 = 0.0054m,
                WeightKg = 0.895m,
                PalletUnitsDefault = 270,
                IsActive = true,
                DefaultUnitPrice = null
            },
            new Product
            {
                InternalCode = "20236",
                Sku = "20236",
                Description = "PEDRA PERFUMADA FLORES DO CAMPO 8X1",
                Unit = "CX",
                PackagingType = "CX",
                VolumeM3 = 0.00141075m,
                WeightKg = 0.295m,
                PalletUnitsDefault = 0,
                IsActive = false,
                DefaultUnitPrice = null
            },
            new Product
            {
                InternalCode = "20237",
                Sku = "20237",
                Description = "PEDRA PERFUMADA FLORES DO CAMPO 24X1",
                Unit = "CX",
                PackagingType = "CX",
                VolumeM3 = 0.0054m,
                WeightKg = 0.895m,
                PalletUnitsDefault = 270,
                IsActive = false,
                DefaultUnitPrice = null
            }
            });
        }

        if (!await db.SkuRules.AnyAsync())
        {
            db.SkuRules.AddRange(new[]
            {
            new SkuRule
            {
                Sku = "12025",
                IsValidForSale = true,
                IsInactive = false
            },
            new SkuRule
            {
                Sku = "12026",
                IsValidForSale = true,
                IsInactive = false
            },
            new SkuRule
            {
                Sku = "12027",
                IsValidForSale = false,
                IsInactive = true
            },
            new SkuRule
            {
                Sku = "12028",
                IsValidForSale = false,
                IsInactive = true
            },
            new SkuRule
            {
                Sku = "12037",
                IsValidForSale = false,
                IsInactive = true
            },
            new SkuRule
            {
                Sku = "12950",
                IsValidForSale = false,
                IsInactive = true
            },
            new SkuRule
            {
                Sku = "20102",
                IsValidForSale = false,
                IsInactive = true
            },
            new SkuRule
            {
                Sku = "20106",
                IsValidForSale = false,
                IsInactive = true
            },
            new SkuRule
            {
                Sku = "20109",
                IsValidForSale = true,
                IsInactive = false
            },
            new SkuRule
            {
                Sku = "20202",
                IsValidForSale = false,
                IsInactive = true
            },
            new SkuRule
            {
                Sku = "20221",
                IsValidForSale = false,
                IsInactive = true
            },
            new SkuRule
            {
                Sku = "20223",
                IsValidForSale = false,
                IsInactive = true
            },
            new SkuRule
            {
                Sku = "20230",
                IsValidForSale = false,
                IsInactive = true
            },
            new SkuRule
            {
                Sku = "20231",
                IsValidForSale = true,
                IsInactive = false
            },
            new SkuRule
            {
                Sku = "20232",
                IsValidForSale = false,
                IsInactive = true
            },
            new SkuRule
            {
                Sku = "20233",
                IsValidForSale = false,
                IsInactive = true
            },
            new SkuRule
            {
                Sku = "20234",
                IsValidForSale = false,
                IsInactive = true
            },
            new SkuRule
            {
                Sku = "20235",
                IsValidForSale = true,
                IsInactive = false
            },
            new SkuRule
            {
                Sku = "20236",
                IsValidForSale = false,
                IsInactive = true
            },
            new SkuRule
            {
                Sku = "20237",
                IsValidForSale = false,
                IsInactive = true
            }
            });
        }

        await db.SaveChangesAsync();
    }

    public static readonly string[] SeededSkus =
    {
        "12025", "12026", "12027", "12028", "12037", "12950", "20102", "20106", "20109", "20202", "20221", "20223", "20230", "20231", "20232", "20233", "20234", "20235", "20236", "20237"
    };
}
