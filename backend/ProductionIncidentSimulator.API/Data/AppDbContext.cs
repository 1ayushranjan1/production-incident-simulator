using Microsoft.EntityFrameworkCore;
using ProductionIncidentSimulator.API.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Order> Orders { get; set; }
    public DbSet<ActivationKey> ActivationKeys { get; set; }
    public DbSet<IncidentLog> IncidentLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Table names (PostgreSQL lowercase)
        modelBuilder.Entity<Order>().ToTable("orders");
        modelBuilder.Entity<ActivationKey>().ToTable("activationkeys");
        modelBuilder.Entity<IncidentLog>().ToTable("incidentlogs");

        // Relationship
        modelBuilder.Entity<ActivationKey>()
            .HasOne(a => a.Order)
            .WithMany(o => o.ActivationKeys)
            .HasForeignKey(a => a.OrderId);
    }
}