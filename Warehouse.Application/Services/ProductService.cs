using Microsoft.Extensions.Logging;
using Warehouse.Application.Common.Interfaces;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto;
using Warehouse.Domain.Entities;
using Warehouse.Infrastructure.Extensions;

namespace Warehouse.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IWarehouseRepository warehouseRepository, ILogger<ProductService> logger)
    {
        _warehouseRepository = warehouseRepository;
        _logger = logger;
    }

    public async Task<ProductDto> GetFilteredProductsAsync(
        decimal? minPrice, 
        decimal? maxPrice, 
        string? size, 
        string? highlight)
    {
        try
        {
            _logger.LogInformation("Getting Products from Database...");

            // Fetch all products from DB
            var allProducts = await _warehouseRepository.GetProductsAsync();
            if (allProducts == null)
            {
                _logger.LogError("Failed to retrieve products from the database.");

                return new ProductDto
                {
                    Filter = new ProductFilter(),
                    Products = Enumerable.Empty<Product>()
                };
            }

            // Extract min and max prices
            decimal? overallMinPrice = allProducts.Min(p => p.Price);
            decimal? overallMaxPrice = allProducts.Max(p => p.Price);

            // Extract all sizes
            var allSizes = allProducts.SelectMany(p => p.Sizes).Distinct().ToArray();

            // Extract and split descriptions
            var wordOccurrences = allProducts.GetWordOccurrences();

            // Exctract common words
            var excludedWords = wordOccurrences.Take(5).ToList();
            var commonWords = wordOccurrences.Skip(5).Take(10).Except(excludedWords).ToArray();

            // Filter products
            _logger.LogInformation("Filtering Products...");
            var filteredProducts = allProducts;

            filteredProducts = filteredProducts
                .FilterByMinPrice(minPrice)
                .FilterByMaxPrice(maxPrice)
                .FilterBySize(size)
                .HighlightWords(highlight);

            return new ProductDto
            {
                Filter = new ProductFilter
                {
                    MinPrice = overallMinPrice,
                    MaxPrice = overallMaxPrice,
                    AllSizes = allSizes,
                    CommonWords = commonWords
                },
                Products = filteredProducts,
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting products: {ex.Message}");
            throw;
        }
    }
}
