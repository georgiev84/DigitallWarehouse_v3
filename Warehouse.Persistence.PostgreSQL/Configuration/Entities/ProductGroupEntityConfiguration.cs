using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Entities.Products;

namespace Warehouse.Persistence.PostgreSQL.Configuration.Entities;

public class ProductGroupEntityConfiguration : IEntityTypeConfiguration<ProductGroup>
{
    public void Configure(EntityTypeBuilder<ProductGroup> builder)
    {
        builder.HasKey(pg => new { pg.ProductId, pg.GroupId });

        builder.HasOne(pg => pg.Product)
            .WithMany(p => p.ProductGroups)
            .HasForeignKey(pg => pg.ProductId);

        builder.HasOne(pg => pg.Group)
            .WithMany(g => g.ProductGroups)
            .HasForeignKey(pg => pg.GroupId);
    }
}