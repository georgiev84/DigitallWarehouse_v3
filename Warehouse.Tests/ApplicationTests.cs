using Microsoft.Extensions.Logging;
using Moq;
using Warehouse.Application.Common.Persistence;
using Warehouse.Domain.Entities;
using Warehouse.Infrastructure.Services;

namespace Warehouse.Tests;

public class ApplicationTests
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
        Assert.NotNull(result);
        Assert.Equal("Sample Product 1", testProducts[0].Title);
        Assert.Equal(25.99m, testProducts[0].Price);
        Assert.Contains("Small", testProducts[0].Sizes);
        Assert.Contains("Medium", testProducts[0].Sizes);
        Assert.Equal("Description for Sample Product 1.", testProducts[0].Description);

    }
}