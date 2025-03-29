using Management.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Management.Shared.Infrastructure.MongoEFC.Persistence;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.AddInterceptors();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>().ToCollection("products");
        modelBuilder.Entity<Product>().HasKey(p => p.Id);
        modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired();
        modelBuilder.Entity<Product>().Property(p => p.Description).IsRequired();
        modelBuilder.Entity<Product>().Property(p => p.Price).IsRequired();
        modelBuilder.Entity<Product>().Property(p => p.Stock).IsRequired();
        
    }
}