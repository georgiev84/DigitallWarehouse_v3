using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Exceptions;
using Warehouse.Infrastructure.Services;

namespace Warehouse.Tests;

public class ProductServiceTests
{
    [Fact]
    public async Task GetFilteredProductsAsync_Should_Return_ProductResponse()
    {
        // Arrange
        var mockWarehouseRepository = new Mock<IWarehouseRepository>();
        var mockProductRepository = new Mock<IGenericRepository<Product>>();
        var mockSizeRepository = new Mock<IGenericRepository<Size>>();
        var mockLogger = new Mock<ILogger<ProductService>>();
        var mockIUnitOfWork = new Mock<IUnitOfWork>();

        var testProducts = new List<Product>
        {
            new Product
            {
                Title = "Sample Product 1",
                Price = 25.99m,
                ProductSizes = new List<ProductSize>
                {
                    new ProductSize { Size = new Size { Name = "Small" } },
                    new ProductSize { Size = new Size { Name = "Medium" } }
                },
                Description = "Description for Sample Product 1."
            },
            new Product
            {
                Title = "Sample Product 2",
                Price = 35.49m,
                ProductSizes = new List<ProductSize>
                {
                    new ProductSize { Size = new Size { Name = "Large" } },
                    new ProductSize { Size = new Size { Name = "Extra Large" } }
                },
                Description = "Description for Sample Product 2."
            },
            new Product
            {
                Title = "Sample Product 3",
                Price = 19.99m,
                ProductSizes = new List<ProductSize>
                {
                    new ProductSize { Size = new Size { Name = "Small" } },
                    new ProductSize { Size = new Size { Name = "Medium" } },
                    new ProductSize { Size = new Size { Name = "Large" } }
                },
                Description = "Description for red Product 3."
            }
        };


        mockWarehouseRepository.Setup(repo => repo.GetProductsAsync()).ReturnsAsync(testProducts);

        var warehouseService = new ProductService(mockLogger.Object,);
        var mockRequestItem = new ItemsDto
        {
            MinPrice = 10,
            MaxPrice = 20,
            Size = "Small",
            Highlight = "red"
        };

        // Act
        var result = await warehouseService.GetFilteredProductsAsync(mockRequestItem);

        // Assert
        result.Should().NotBeNull();
        result.Filter.Should().NotBeNull();
        result.Products.Should().NotBeNull().And.HaveCount(1);
        result.Products!.First().Title.Should().Be("Sample Product 3");
        result.Products!.First().Price.Should().Be(19.99m);
        result.Products!.First().ProductSizes.Select(ps => ps.Size.Name).Should().Contain("Small").And.Contain("Medium").And.Contain("Large");
        result.Products!.First().Description.Should().Be("Description for <em>red</em> Product 3.");
    }

    [Fact]
    public async Task GetFilteredProductsAsync_HighlightWords_Should_Highlight_Matching_Words()
    {
        // Arrange
        var mockWarehouseRepository = new Mock<IWarehouseRepository>();
        var mockLogger = new Mock<ILogger<ProductService>>();
        var mockProductRepository = new Mock<IGenericRepository<Product>>();
        var mockSizeRepository = new Mock<IGenericRepository<Size>>();

        var testProducts = new List<Product>
        {
            new Product
            {
                Title = "Sample Product 1",
                Price = 25.99m,
                ProductSizes = new List<ProductSize>
                {
                    new ProductSize { Size = new Size { Name = "Small" } },
                    new ProductSize { Size = new Size { Name = "Medium" } }
                },
                Description = "Description for Sample Product 1."
            }
        };


        mockWarehouseRepository.Setup(repo => repo.GetProductsAsync()).ReturnsAsync(testProducts);

        var warehouseService = new ProductService(mockLogger.Object, mockProductRepository.Object, mockSizeRepository.Object);
        var mockRequestItem = new ItemsDto
        {
            MinPrice = null,
            MaxPrice = null,
            Size = null,
            Highlight = "red"
        };
        // Act
        var result = await warehouseService.GetFilteredProductsAsync(mockRequestItem);

        // Assert
        result.Should().NotBeNull();
        result.Products.Should().NotBeNull().And.HaveCount(1);
        result.Products.First().Description.Should().Contain("<em>red</em>").And.NotContain("Description for Sample Product 1.");
    }

    [Fact]
    public async Task GetFilteredProductsAsync_ReturnProductNotFounException()
    {
        // Arrange
        var mockWarehouseRepository = new Mock<IWarehouseRepository>();
        var mockLogger = new Mock<ILogger<ProductService>>();
        var mockProductRepository = new Mock<IGenericRepository<Product>>();
        var mockSizeRepository = new Mock<IGenericRepository<Size>>();

        mockWarehouseRepository.Setup(repo => repo.GetProductsAsync()).ReturnsAsync((List<Product>)null);

        var warehouseService = new ProductService(mockLogger.Object, mockProductRepository.Object, mockSizeRepository.Object);
        var mockRequestItem = new ItemsDto
        {
            MinPrice = null,
            MaxPrice = null,
            Size = null,
            Highlight = null
        };

        // Act & Assert
        await Assert.ThrowsAsync<ProductNotFoundException>(async () =>
        {
            await warehouseService.GetFilteredProductsAsync(mockRequestItem);
        });
    }
}