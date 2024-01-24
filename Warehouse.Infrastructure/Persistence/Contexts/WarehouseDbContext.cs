using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Warehouse.Domain.Entities;
using Warehouse.Infrastructure.Configuration.Entities;

namespace Warehouse.Infrastructure.Persistence.Contexts;
public class WarehouseDbContext : DbContext
{
    public WarehouseDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }
    public DbSet<OrderStatus> OrderStatus { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Size> Sizes { get; set; }
    public DbSet<ProductSize> ProductSizes { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductGroup> ProductGroups { get; set; }
    public DbSet<Brand> Brands { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserEntityConfiguration());
        builder.ApplyConfiguration(new ProductEntityConfiguration());
        builder.ApplyConfiguration(new ProductSizeEntityConfiguration());
        builder.ApplyConfiguration(new ProductGroupEntityConfiguration());
        builder.ApplyConfiguration(new OrderEntityConfiguration());
        builder.ApplyConfiguration(new OrderDetailsEntityConfiguration());

        // Seed Brands
        builder.Entity<Brand>().HasData(
            new Brand { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Brand 1" },
            new Brand { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Brand 2" }
        );

        // Seed Sizes
        builder.Entity<Size>().HasData(
            new Size { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Small" },
            new Size { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Medium" },
            new Size { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "Large" }
        );

        // Seed Products
        builder.Entity<Product>().HasData(
            new Product
            {
                Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                Title = "Product 1",
                Description = "Description for Product 1",
                Price = 29.99m,
                BrandId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                SizeId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                // Include other product properties
            },
            new Product
            {
                Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                Title = "Product 2",
                Description = "Description for Product 2",
                Price = 39.99m,
                BrandId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                SizeId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                // Include other product properties
            }
        );

        // Seed Groups
        builder.Entity<Group>().HasData(
            new Group { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), Name = "Group 1" },
            new Group { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), Name = "Group 2" }
        );

        // Seed ProductGroups (many-to-many)
        builder.Entity<ProductGroup>().HasData(
            new ProductGroup { ProductId = Guid.Parse("66666666-6666-6666-6666-666666666666"), GroupId = Guid.Parse("88888888-8888-8888-8888-888888888888") }
        // Add more ProductGroups as needed
        );

        // Seed ProductSizes (many-to-many)
        builder.Entity<ProductSize>().HasData(
            new ProductSize { ProductId = Guid.Parse("66666666-6666-6666-6666-666666666666"), SizeId = Guid.Parse("33333333-3333-3333-3333-333333333333"), QuantityInStock = 50 }
        // Add more ProductSizes as needed
        );




        base.OnModelCreating(builder);
    }
}
