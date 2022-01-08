using DemoTransaction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoTransaction.Infrastructure.Data.Mappings;

public class ProductMapping : IEntityTypeConfiguration<ProductModel>
{
    public void Configure(EntityTypeBuilder<ProductModel> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Description).HasColumnType("VARCHAR(60)");
        builder.Property(p => p.Quantity).HasDefaultValue(1).IsRequired();
        builder.Property(p => p.Price).HasDefaultValue(0).IsRequired();

        // 1 : N => Product : Items
        builder.HasMany(p => p.Items)
            .WithOne(p => p.Product)
            .HasForeignKey(c => c.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("Products");
    }
}
