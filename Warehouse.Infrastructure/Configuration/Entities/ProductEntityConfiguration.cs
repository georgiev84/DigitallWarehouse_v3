using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.Configuration.Entities;
public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasOne(p => p.Brand)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.BrandId);

        builder.Property(p => p.Price)
            .HasColumnType("decimal(18, 2)");
    }
}
