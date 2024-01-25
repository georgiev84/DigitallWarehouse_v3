using System.Text.RegularExpressions;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Models;

namespace Warehouse.Infrastructure.Extensions;

public static class ProductExtensions
{
    public static IEnumerable<ProductDetailsDto> HighlightWords(this IEnumerable<ProductDetailsDto> products, string? highlight)
    {
        if (!string.IsNullOrEmpty(highlight))
        {
            var highlightWords = highlight.Split(',').Select(w => w.Trim()).ToList();
            var updatedProducts = new List<ProductDetailsDto>();

            foreach (var product in products)
            {
                var updatedProduct = new ProductDetailsDto
                {
                    Id = product.Id,
                    Title = product.Title,
                    Description = product.Description,
                    Price = product.Price,
                    Brand = product.Brand,
                    Groups = new List<string>(product.Groups),
                    Sizes = new List<SizeDto>(product.Sizes)
                };

                foreach (var word in highlightWords)
                {
                    updatedProduct.Description = Regex.Replace(
                        updatedProduct.Description,
                        $@"\b({Regex.Escape(word)}|{Regex.Escape(word.ToLowerInvariant())})\b",
                        match => $"<em>{match.Value}</em>",
                        RegexOptions.IgnoreCase
                    );
                }

                updatedProducts.Add(updatedProduct);
            }

            return updatedProducts;
        }

        return products;
    }


    public static IEnumerable<ProductDetailsDto> FilterBySize(this IEnumerable<ProductDetailsDto> products, string? size)
    {
        if (string.IsNullOrEmpty(size))
        {
            return products;
        }

        var filteredProducts = products.Where(p =>
            p.Sizes != null && 
            p.Sizes.Any(sizeName => string.Equals(sizeName.Name, size, StringComparison.OrdinalIgnoreCase)));

        return filteredProducts;
    }

    public static IEnumerable<ProductDetailsDto> FilterByMinPrice(this IEnumerable<ProductDetailsDto> products, decimal? minPrice)
    {
        if (minPrice.HasValue)
        {
            return products.Where(p => p.Price >= minPrice.Value);
        }
        return products;
    }

    public static IEnumerable<ProductDetailsDto> FilterByMaxPrice(this IEnumerable<ProductDetailsDto> products, decimal? maxPrice)
    {
        if (maxPrice.HasValue)
        {
            return products.Where(p => p.Price <= maxPrice.Value);
        }
        return products;
    }

    public static List<string> GetWordOccurrences(this IEnumerable<ProductDetailsDto> products)
    {
        var allDescriptions = products.Select(p => p.Description).ToList();

        return allDescriptions
            .SelectMany(desc => desc.Split(new[] { ' ', '.', ',' }, StringSplitOptions.RemoveEmptyEntries))
            .GroupBy(word => word.ToLower())
            .Select(group => new { Word = group.Key, Count = group.Count() })
            .OrderBy(x => x.Word)
            .OrderByDescending(x => x.Count)
            .Select(x => x.Word)
            .ToList();
    }
}
