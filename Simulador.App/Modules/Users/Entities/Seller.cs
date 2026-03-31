using Simulador.App.Auth;
using Simulador.App.Modules.Regional.Entities;

public class SellerRegional
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string SellerUserId { get; set; } = default!;
    public Guid RegionalId { get; set; }

    public ApplicationUser SellerUser { get; set; } = default!;
    public Regional Regional { get; set; } = default!;
}