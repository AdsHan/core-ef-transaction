using DemoTransaction.Domain.Entities;
using DemoTransaction.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DemoTransaction.Infrastructure.Data;

public class OrderDbContext : DbContext
{

    public OrderDbContext()
    {

    }

    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {

    }

    public DbSet<OrderModel> Orders { get; set; }
    public DbSet<OrderItemModel> OrderItems { get; set; }
    public DbSet<ProductModel> Products { get; set; }
    public DbSet<CustomerModel> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<string>()
            .HaveMaxLength(90);

        configurationBuilder
            .Properties<EntityStatusEnum>()
            .HaveConversion<int>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

}

