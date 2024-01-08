using Warehouse.Application.Common.Interfaces;
using Warehouse.Application.Common.Persistence;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Responses;

namespace Warehouse.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IWarehouseRepository _warehouseRepository;

    public ProductService(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }

    public async Task<ProductResponse> GetFilteredProductsAsync(decimal? minPrice, decimal? maxPrice, string size, string highlight)
    {
        var products = await _warehouseRepository.GetProductsAsync(minPrice, maxPrice, size);

        if (!string.IsNullOrEmpty(highlight))
        {
            products = HighlightWords(products, highlight);
        }
        // Extracting min and max prices
        decimal? overallMinPrice = products.Min(p => p.Price);
        decimal? overallMaxPrice = products.Max(p => p.Price);

        // Extract sizes
        var allSizes = products.SelectMany(p => p.Sizes).Distinct().ToArray();

        // Extract and split descriptions
        var allDescriptions = products.Select(p => p.Description).ToList();
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

        return new ProductResponse
        {
            Filter = new ProductFilter
            {
                MinPrice = overallMinPrice,
                MaxPrice = overallMaxPrice,
                AllSizes = allSizes,
                CommonWords = commonWords
            },
            Products = products,
        };
    }

    private IEnumerable<Product> HighlightWords(IEnumerable<Product> products, string highlight)
    {
        var highlightWords = highlight.Split(',').Select(w => w.Trim()).ToList();
        foreach (var product in products)
        {
            foreach (var word in highlightWords)
            {
                product.Description = product.Description.Replace(word, $"<em>{word}</em>");
            }
        }
        return products;
    }
}
