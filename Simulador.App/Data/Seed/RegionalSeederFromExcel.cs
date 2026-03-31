using Microsoft.EntityFrameworkCore;
using Simulador.App.Data;
using Simulador.App.Modules.Regional.Entities;

namespace Simulador.App.Data.Seed;

public static class RegionalSeederFromExcel
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.Regionals.AnyAsync())
            return;

        db.Regionals.AddRange(new[]
        {
            new Regional { Code = "101000000", Name = "DIV. CN", IsActive = true },
            new Regional { Code = "101001000", Name = "DIV. CN-AREA CO", IsActive = true },
            new Regional { Code = "DIV. CN-AREA ES", Name = "DIV. CN-AREA ES", IsActive = true },
            new Regional { Code = "DIV. CN-AREA GO", Name = "DIV. CN-AREA GO", IsActive = true },
            new Regional { Code = "DIV. CN-AREA MG", Name = "DIV. CN-AREA MG", IsActive = true },
            new Regional { Code = "101004000", Name = "DIV. CN-AREA MT/MS", IsActive = true },
            new Regional { Code = "101005000", Name = "DIV. CN-AREA NORTE", IsActive = true },
            new Regional { Code = "DIV. CN-AREA TRIANGULO", Name = "DIV. CN-AREA TRIANGULO", IsActive = true },
            new Regional { Code = "109900000", Name = "DIV. DISTRIBUICAO", IsActive = true },
            new Regional { Code = "DIV. EXP", Name = "DIV. EXP", IsActive = true },
            new Regional { Code = "109001000", Name = "DIV. EXP-AREA EXPORTACAO", IsActive = true },
            new Regional { Code = "106000000", Name = "DIV. LE", IsActive = true },
            new Regional { Code = "106002000", Name = "DIV. LE-AREA ES", IsActive = true },
            new Regional { Code = "106003000", Name = "DIV. LE-AREA MG", IsActive = true },
            new Regional { Code = "106001000", Name = "DIV. LE-AREA TRIANGULO", IsActive = true },
            new Regional { Code = "DIV. LIMPPANO", Name = "DIV. LIMPPANO", IsActive = true },
            new Regional { Code = "108001000", Name = "DIV. LIMPPANO-AREA VENDA DIRETA", IsActive = true },
            new Regional { Code = "102000000", Name = "DIV. NE", IsActive = true },
            new Regional { Code = "102001000", Name = "DIV. NE-AREA CENTRO", IsActive = true },
            new Regional { Code = "102002000", Name = "DIV. NE-AREA LESTE", IsActive = true },
            new Regional { Code = "102003000", Name = "DIV. NE-AREA NORTE", IsActive = true },
            new Regional { Code = "102004000", Name = "DIV. NE-AREA SUL", IsActive = true },
            new Regional { Code = "103000000", Name = "DIV. RJ", IsActive = true },
            new Regional { Code = "103002000", Name = "DIV. RJ-AREA CAPITAL", IsActive = true },
            new Regional { Code = "103001000", Name = "DIV. RJ-AREA VAREJO G RIO", IsActive = true },
            new Regional { Code = "103003000", Name = "DIV. RJ-AREA VAREJO INTERIOR", IsActive = true },
            new Regional { Code = "104000000", Name = "DIV. SP", IsActive = true },
            new Regional { Code = "104001000", Name = "DIV. SP-AREA CONTAS ESPECIAIS", IsActive = true },
            new Regional { Code = "104007000", Name = "DIV. SP-AREA DISTRIBUIDOR", IsActive = true },
            new Regional { Code = "104002000", Name = "DIV. SP-AREA INTERIOR I", IsActive = true },
            new Regional { Code = "104003000", Name = "DIV. SP-AREA INTERIOR II", IsActive = true },
            new Regional { Code = "104004000", Name = "DIV. SP-AREA KA", IsActive = true },
            new Regional { Code = "104005000", Name = "DIV. SP-AREA MEDIO VAREJO", IsActive = true },
            new Regional { Code = "104006000", Name = "DIV. SP-AREA VAREJO", IsActive = true },
            new Regional { Code = "105000000", Name = "DIV. SUL", IsActive = true },
            new Regional { Code = "105005000", Name = "DIV. SUL- AREA RS II", IsActive = true },
            new Regional { Code = "105002000", Name = "DIV. SUL-AREA PR", IsActive = true },
            new Regional { Code = "DIV. SUL-AREA PR INTERIOR", Name = "DIV. SUL-AREA PR INTERIOR", IsActive = true },
            new Regional { Code = "105003000", Name = "DIV. SUL-AREA RS", IsActive = true },
            new Regional { Code = "105001000", Name = "DIV. SUL-AREA SC", IsActive = true }
        });

        await db.SaveChangesAsync();
    }
}