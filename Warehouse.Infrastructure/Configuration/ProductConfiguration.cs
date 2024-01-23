using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.Configuration;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Brand -> Product
        // One to many
        builder.HasOne(p => p.Brand)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.SizeId);
    }
}
