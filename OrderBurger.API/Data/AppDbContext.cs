using Microsoft.EntityFrameworkCore;
using OrderBurger.API.Models;

namespace OrderBurger.API.Data;

public sealed class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}
   
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>().HaveMaxLength(256);
    }
}