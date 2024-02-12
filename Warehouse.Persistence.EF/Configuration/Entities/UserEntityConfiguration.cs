using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Entities;

namespace Warehouse.Persistence.EF.Configuration.Entities;
public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Email).HasMaxLength(50).IsRequired();
        builder.Property(u => u.Password).HasMaxLength(50).IsRequired();
        builder.Property(u => u.Address).HasMaxLength(255);
        builder.Property(u => u.Phone).HasMaxLength(50);
        builder.HasMany(u => u.Orders)
            .WithOne(u => u.User)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(u => u.Basket)
            .WithOne(b => b.User)
            .HasForeignKey<Basket>(b => b.UserId);
    }
}
