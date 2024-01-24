using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.Configuration.Entities;
public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Email).HasMaxLength(50).IsRequired();
        builder.Property(u => u.Password).IsRequired();
        builder.HasMany(u => u.Orders)
            .WithOne(u => u.User)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
