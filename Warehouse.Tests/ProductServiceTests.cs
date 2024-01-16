using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities;
using Warehouse.Infrastructure.Services;

namespace Warehouse.Tests;

public class ProductServiceTests
{
    [Fact]
    public async Task GetFilteredProductsAsync_Should_Return_ProductResponse()
    {
        // Arrange
        var mockWarehouseRepository = new Mock<IWarehouseRepository>();
        var mockLogger = new Mock<ILogger<ProductService>>();

        var testProducts = new List<Product>
        {
            new Product
            {
                Title = "Sample Product 1",
                Price = 25.99m,
                Sizes = new List<string> { "Small", "Medium" },
                Description = "Description for Sample Product 1."
            },
            new Product
            {
                Title = "Sample Product 2",
                Price = 35.49m,
                Sizes = new List<string> { "Large", "Extra Large" },
                Description = "Description for Sample Product 2."
            },
            new Product
            {
                Title = "Sample Product 3",
                Price = 19.99m,
                Sizes = new List<string> { "Small", "Medium", "Large" },
                Description = "Description for Sample Product 3."
            }
        };

        mockWarehouseRepository.Setup(repo => repo.GetProductsAsync()).ReturnsAsync(testProducts);

        var warehouseService = new ProductService(mockWarehouseRepository.Object, mockLogger.Object);

        // Act
        var result = await warehouseService.GetFilteredProductsAsync(10, 100, "Small", "red");

        // Assert
        result.Should().NotBeNull();
        result.Filter.Should().NotBeNull();
        result.Products.Should().NotBeNull().And.HaveCount(2);
        result.Products!.First().Title.Should().Be("Sample Product 1");
        result.Products!.First().Price.Should().Be(25.99m);
        result.Products!.First().Sizes.Should().Contain("Small").And.Contain("Medium");
        result.Products!.First().Description.Should().Be("Description for Sample Product 1.");
    }

    [Fact]
    public async Task GetFilteredProductsAsync_HighlightWords_Should_Highlight_Matching_Words()
    {
        // Arrange
        var mockWarehouseRepository = new Mock<IWarehouseRepository>();
        var mockLogger = new Mock<ILogger<ProductService>>();

        var testProducts = new List<Product>
    {
        new Product
        {
            Title = "Sample Product 1",
            Price = 25.99m,
            Sizes = new List<string> { "Small", "Medium" },
            Description = "Description for Sample Product 1."
        }
    };

        mockWarehouseRepository.Setup(repo => repo.GetProductsAsync()).ReturnsAsync(testProducts);

        var warehouseService = new ProductService(mockWarehouseRepository.Object, mockLogger.Object);

        // Act
        var result = await warehouseService.GetFilteredProductsAsync(null, null, null, "Sample");

        // Assert
        result.Should().NotBeNull();
        result.Products.Should().NotBeNull().And.HaveCount(1);
        result.Products.First().Description.Should().Contain("<em>Sample</em>").And.NotContain("Description for Sample Product 1.");
    }

    [Fact]
    public async Task GetFilteredProductsAsync_ReturnEmptyProductResponse()
    {
        // Arrange
        var mockWarehouseRepository = new Mock<IWarehouseRepository>();
        var mockLogger = new Mock<ILogger<ProductService>>();

        mockWarehouseRepository.Setup(repo => repo.GetProductsAsync()).ReturnsAsync((List<Product>)null);

        var warehouseService = new ProductService(mockWarehouseRepository.Object, mockLogger.Object);

        // Act
        var result = await warehouseService.GetFilteredProductsAsync(null, null, null, null);

        // Assert
        result.Should().NotBeNull();
        result.Filter.Should().NotBeNull();
        result.Filter.AllSizes.Should().BeNull();
        result.Filter.CommonWords.Should().BeNull();
        result.Filter.MaxPrice.Should().BeNull();
        result.Filter.MinPrice.Should().BeNull();
        result.Products.Should().BeEmpty();

    }
}