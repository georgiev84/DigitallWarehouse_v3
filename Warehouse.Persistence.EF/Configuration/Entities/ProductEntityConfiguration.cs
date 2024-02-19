using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Entities.Products;
using Warehouse.Persistence.EF.Configuration.Contstants;

namespace Warehouse.Persistence.EF.Configuration.Entities;

public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasOne(p => p.Brand)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.BrandId);

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Description)
            .HasMaxLength(1000);

        builder.Property(p => p.Price)
            .HasColumnType(ColumnTypeConstants.DecimalColumnType);

        builder.Property(p => p.IsDeleted)
        .HasColumnType("bit")
        .HasDefaultValue(false);
    }
}