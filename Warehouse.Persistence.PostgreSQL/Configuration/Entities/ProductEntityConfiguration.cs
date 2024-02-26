using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Entities.Products;
using Warehouse.Persistence.PostgreSQL.Configuration.Contstants;

namespace Warehouse.Persistence.PostgreSQL.Configuration.Entities;

public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasOne(p => p.Brand)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.BrandId);

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(ColumnTypeConstants.TitleMaxLength);

        builder.Property(p => p.Description)
            .HasMaxLength(ColumnTypeConstants.DescriptionMaxLength);

        builder.Property(p => p.Price)
            .HasColumnType(ColumnTypeConstants.DecimalColumnType);

        builder.Property(p => p.IsDeleted)
        .HasColumnType(ColumnTypeConstants.BitColumnType)
        .HasDefaultValue(false);
    }
}