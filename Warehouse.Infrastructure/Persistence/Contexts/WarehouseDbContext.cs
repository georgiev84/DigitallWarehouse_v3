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
            new Brand { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Adidas" },
            new Brand { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Nike" }
        );

        // Seed Sizes
        builder.Entity<Size>().HasData(
            new Size { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Small" },
            new Size { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Medium" },
            new Size { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "Large" }
        );

        // Seed OrderStatus
        builder.Entity<OrderStatus>().HasData(
            new OrderStatus { Id = Guid.Parse("11111111-2222-2321-2321-111111111111"), Name = "Pending" },
            new OrderStatus { Id = Guid.Parse("22222222-1111-1234-4321-222222222222"), Name = "Processing" },
            new OrderStatus { Id = Guid.Parse("33333333-3322-1122-4444-333333333333"), Name = "Shipped" },
            new OrderStatus { Id = Guid.Parse("44444444-5555-5555-6666-666666666666"), Name = "Completed" },
            new OrderStatus { Id = Guid.Parse("77777777-7777-7777-8888-888888888888"), Name = "Cancelled" }

        );

        builder.Entity<User>().HasData(
            new User
            {
                Id = Guid.Parse("11111111-2222-2321-2321-111111111456"),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123", 
                Phone = "123-456-7890",
                Address = "123 Main Street, City, Country",
                Orders = new List<Order>() 
            }
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

            },
            new Product
            {
                Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                Title = "Product 4",
                Description = "Description for Product 4",
                Price = 49.99m,
                BrandId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            },
            new Product
            {
                Id = Guid.Parse("12345678-1234-5678-9012-345678901234"),
                Title = "Product 5",
                Description = "Description for Product 5",
                Price = 59.99m,
                BrandId = Guid.Parse("11111111-1111-1111-1111-111111111111"),

            },
            new Product
            {                   
                Id = Guid.Parse("23456789-2345-6789-0123-456789012345"),
                Title = "Classic Jeans",
                Description = "Timeless classic jeans in blue for a casual and versatile look.",
                Price = 34.99m,
                BrandId = Guid.Parse("22222222-2222-2222-2222-222222222222"),

            }
        );

        // Seed Groups
        builder.Entity<Group>().HasData(
            new Group { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), Name = "Male" },
            new Group { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), Name = "Female" }
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
            new ProductSize { ProductId = Guid.Parse("88888888-8888-8888-8888-888888888888"), SizeId = Guid.Parse("44444444-4444-4444-4444-444444444444"), QuantityInStock = 20 },

            new ProductSize { ProductId = Guid.Parse("99999999-9999-9999-9999-999999999999"), SizeId = Guid.Parse("55555555-5555-5555-5555-555555555555"), QuantityInStock = 30 },

            new ProductSize { ProductId = Guid.Parse("12345678-1234-5678-9012-345678901234"), SizeId = Guid.Parse("55555555-5555-5555-5555-555555555555"), QuantityInStock = 30 },
            new ProductSize { ProductId = Guid.Parse("12345678-1234-5678-9012-345678901234"), SizeId = Guid.Parse("44444444-4444-4444-4444-444444444444"), QuantityInStock = 20 },



            new ProductSize { ProductId = Guid.Parse("23456789-2345-6789-0123-456789012345"), SizeId = Guid.Parse("55555555-5555-5555-5555-555555555555"), QuantityInStock = 30 },
            new ProductSize { ProductId = Guid.Parse("23456789-2345-6789-0123-456789012345"), SizeId = Guid.Parse("44444444-4444-4444-4444-444444444444"), QuantityInStock = 20 }
        );

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}
