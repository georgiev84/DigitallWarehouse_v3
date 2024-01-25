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
                Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                Title = "Product 3",
                Description = "Description for Product 3",
                Price = 19.99m,
                BrandId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                SizeId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                // Include other product properties
            },
            new Product
            {
                Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                Title = "Product 4",
                Description = "Description for Product 4",
                Price = 49.99m,
                BrandId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                SizeId = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                // Include other product properties
            },
            new Product
            {
                Id = Guid.Parse("12345678-1234-5678-9012-345678901234"),
                Title = "Product 5",
                Description = "Description for Product 5",
                Price = 59.99m,
                BrandId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                SizeId = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                // Include other product properties
            },
            new Product
            {
                Id = Guid.Parse("23456789-2345-6789-0123-456789012345"),
                Title = "Product 6",
                Description = "Description for Product 6",
                Price = 34.99m,
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

        // Seed ProductGroups (many-to-many) for the new products
        builder.Entity<ProductGroup>().HasData(
            new ProductGroup { ProductId = Guid.Parse("88888888-8888-8888-8888-888888888888"), GroupId = Guid.Parse("99999999-9999-9999-9999-999999999999") },
            new ProductGroup { ProductId = Guid.Parse("99999999-9999-9999-9999-999999999999"), GroupId = Guid.Parse("88888888-8888-8888-8888-888888888888") },
            new ProductGroup { ProductId = Guid.Parse("12345678-1234-5678-9012-345678901234"), GroupId = Guid.Parse("88888888-8888-8888-8888-888888888888") },
            new ProductGroup { ProductId = Guid.Parse("23456789-2345-6789-0123-456789012345"), GroupId = Guid.Parse("99999999-9999-9999-9999-999999999999") }
        // Add more ProductGroups as needed
        );

        // Seed ProductSizes (many-to-many)
        builder.Entity<ProductSize>().HasData(
            new ProductSize { ProductId = Guid.Parse("88888888-8888-8888-8888-888888888888"), SizeId = Guid.Parse("55555555-5555-5555-5555-555555555555"), QuantityInStock = 30 },
            new ProductSize { ProductId = Guid.Parse("99999999-9999-9999-9999-999999999999"), SizeId = Guid.Parse("44444444-4444-4444-4444-444444444444"), QuantityInStock = 20 },
            new ProductSize { ProductId = Guid.Parse("12345678-1234-5678-9012-345678901234"), SizeId = Guid.Parse("33333333-3333-3333-3333-333333333333"), QuantityInStock = 15 },
            new ProductSize { ProductId = Guid.Parse("23456789-2345-6789-0123-456789012345"), SizeId = Guid.Parse("55555555-5555-5555-5555-555555555555"), QuantityInStock = 25 }
        // Add more ProductSizes as needed
        );


        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseLazyLoadingProxies(); // Enable lazy loading
        base.OnConfiguring(optionsBuilder);
    }
}
