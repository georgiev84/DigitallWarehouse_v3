using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Entities.Orders;
using Warehouse.Persistence.PostgreSQL.Configuration.Contstants;

namespace Warehouse.Persistence.PostgreSQL.Configuration.Entities;

public class OrderLinesEntityConfiguration : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder.HasKey(od => new { od.OrderId, od.ProductId });

        builder.HasOne(od => od.Order)
            .WithMany(o => o.OrderLines)
            .HasForeignKey(od => od.OrderId);

        builder.HasOne(od => od.Product)
            .WithMany(p => p.OrderLines)
            .HasForeignKey(od => od.ProductId);

        builder.Property(p => p.Price)
            .HasColumnType(ColumnTypeConstants.DecimalColumnType);

        builder.HasOne(od => od.Size)
        .WithMany(s => s.OrderLines)
        .HasForeignKey(od => od.SizeId);
    }
}