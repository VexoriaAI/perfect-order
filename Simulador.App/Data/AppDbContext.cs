using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Simulador.App.Auth;
using Simulador.App.Modules.Catalog.Entities;
using Simulador.App.Modules.Config.Entities;
using Simulador.App.Modules.Customers.Entities;
using Simulador.App.Modules.Simulations.Entities;
using Simulador.App.Modules.Vehicles.Entities;

namespace Simulador.App.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<SkuRule> SkuRules => Set<SkuRule>();
    public DbSet<PriceAverage> PriceAverages => Set<PriceAverage>();

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<CustomerRules> CustomerRules => Set<CustomerRules>();
    public DbSet<CustomerAllowedVehicle> CustomerAllowedVehicles => Set<CustomerAllowedVehicle>();

    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<SystemConfig> SystemConfigs => Set<SystemConfig>();

    public DbSet<SimulationRun> SimulationRuns => Set<SimulationRun>();
    public DbSet<SimulationItem> SimulationItems => Set<SimulationItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>(b =>
        {
            b.Property(x => x.FullName).HasMaxLength(150);
        });

        // Products
        modelBuilder.Entity<Product>()
            .HasIndex(x => x.Sku)
            .IsUnique();

        modelBuilder.Entity<SkuRule>()
            .HasIndex(x => x.Sku)
            .IsUnique();

        modelBuilder.Entity<PriceAverage>()
            .HasIndex(x => new { x.CompanyBilling, x.Sku })
            .IsUnique();

        // Customers
        modelBuilder.Entity<Customer>()
            .HasIndex(x => x.CustomerCode)
            .IsUnique();

        modelBuilder.Entity<Customer>()
            .HasOne(x => x.Rules)
            .WithOne(x => x.Customer)
            .HasForeignKey<CustomerRules>(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CustomerAllowedVehicle>()
            .HasIndex(x => new { x.CustomerId, x.VehicleName });

        // Vehicles
        modelBuilder.Entity<Vehicle>()
            .HasIndex(x => x.Name)
            .IsUnique();

        // Config
        modelBuilder.Entity<SystemConfig>()
            .HasIndex(x => x.Key)
            .IsUnique();

        // Simulations
        modelBuilder.Entity<SimulationItem>()
            .HasOne(x => x.SimulationRun)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.SimulationRunId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}