using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.Configuration.Entities;
public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
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
            .HasColumnType("decimal(18, 2)");
    }
}
