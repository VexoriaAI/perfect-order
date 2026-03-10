namespace Simulador.App.Modules.Config.Entities;

public class SystemConfig
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Key { get; set; } = default!;  // "DEFAULT"
    public decimal DefaultLossPercent { get; set; } = 0.15m; // carga batida
    public decimal FreightIdealPercent { get; set; } = 0.06m;
    public decimal FreightAcceptablePercent { get; set; } = 0.08m;

    // política quando não existe preço médio (não vendedor)
    // 0 = Flexível (valor 0 + warning), 1 = Estrito (erro)
    public int MissingPricePolicy { get; set; } = 0;
}