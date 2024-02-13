using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Entities;

namespace Warehouse.Persistence.EF.Configuration.Entities;

public class OrderLinesEntityConfiguration : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder.ToTable("OrderLines");
        builder.HasKey(od => new { od.OrderId, od.ProductId });

        builder.HasOne(od => od.Order)
            .WithMany(o => o.OrderLines)
            .HasForeignKey(od => od.OrderId);

        builder.HasOne(od => od.Product)
            .WithMany(p => p.OrderLines)
            .HasForeignKey(od => od.ProductId);

        builder.Property(p => p.Price)
            .HasColumnType("decimal(18, 2)");

        builder.HasOne(od => od.Size)
        .WithMany(s => s.OrderLines)
        .HasForeignKey(od => od.SizeId);
    }
}