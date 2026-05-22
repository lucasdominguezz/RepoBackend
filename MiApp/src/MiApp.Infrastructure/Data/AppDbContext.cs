using Microsoft.EntityFrameworkCore;
using MiApp.Domain.Entities;

namespace MiApp.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");
            entity.HasKey(product => product.Id);
            entity.Property(product => product.Name).IsRequired().HasMaxLength(100);
            entity.Property(product => product.Price).HasColumnType("decimal(18,2)");
            entity.Property(product => product.Stock).IsRequired();
        });
    }
}
