using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Entities.Baskets;
using Warehouse.Persistence.PostgreSQL.Configuration.Contstants;

namespace Warehouse.Persistence.PostgreSQL.Configuration.Entities;

public class BasketLineEntityConfiguration : IEntityTypeConfiguration<BasketLine>
{
    public void Configure(EntityTypeBuilder<BasketLine> builder)
    {
        builder.HasKey(bl => bl.Id);

        builder.HasOne(bl => bl.Size)
            .WithMany(s => s.BasketLines)
            .HasForeignKey(bl => bl.SizeId);

        builder.HasOne(bl => bl.Product)
            .WithMany(p => p.BasketLines)
            .HasForeignKey(bl => bl.ProductId);

        builder.Property(p => p.Price)
            .HasColumnType(ColumnTypeConstants.DecimalColumnType);
    }
}