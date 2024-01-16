using Microsoft.Extensions.Logging;
using Warehouse.Application.Common.Interfaces;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Responses;
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

    public async Task<ProductDomainModel> GetFilteredProductsAsync(decimal? minPrice, decimal? maxPrice, string size, string highlight)
    {
        try
        {
            _logger.LogInformation("Getting Products from Database...");

            // Fetch all products from DB
            var allProducts = await _warehouseRepository.GetProductsAsync();
            if (allProducts == null)
            {
                _logger.LogError("Failed to retrieve products from the database.");

                return new ProductDomainModel
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
            var allDescriptions = allProducts.Select(p => p.Description).ToList();
            var wordOccurrences = allDescriptions
                .SelectMany(desc => desc.Split(new[] { ' ', '.', ',' }, StringSplitOptions.RemoveEmptyEntries))
                .GroupBy(word => word.ToLower())
                .Select(group => new { Word = group.Key, Count = group.Count() })
                .OrderByDescending(x => x.Count)
                .Select(x => x.Word)
                .ToList();

            // Exctract common words
            var excludedWords = wordOccurrences.Take(5).ToList();
            var commonWords = wordOccurrences.Skip(5).Take(10).Except(excludedWords).ToArray();

            // Filter products
            _logger.LogInformation("Filtering Products...");
            var filteredProducts = allProducts;

            filteredProducts = filteredProducts.FilterByMinPrice(minPrice);
            filteredProducts = filteredProducts.FilterByMaxPrice(maxPrice);
            filteredProducts = filteredProducts.FilterBySize(size);
            filteredProducts = filteredProducts.HighlightWords(highlight);

            return new ProductDomainModel
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
