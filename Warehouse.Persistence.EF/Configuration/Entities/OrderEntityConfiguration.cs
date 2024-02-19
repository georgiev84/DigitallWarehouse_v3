using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Entities;
using Warehouse.Persistence.EF.Configuration.Contstants;

namespace Warehouse.Persistence.EF.Configuration.Entities;

public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(o => o.PaymentId).IsRequired(false);
        builder.Property(o => o.UserId).IsRequired(false);

        builder.HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);

        builder.HasOne(o => o.Status)
            .WithMany(s => s.Orders)
            .HasForeignKey(o => o.StatusId);

        builder.HasOne(o => o.Payment)
            .WithOne(p => p.Order)
            .HasForeignKey<Payment>(o => o.PaymentId);

        builder.Property(p => p.TotalAmount)
            .HasColumnType(ColumnTypeConstants.DecimalColumnType);

        builder.Property(o => o.IsDeleted)
            .HasColumnType("bit")
            .HasDefaultValue(false);
    }
}