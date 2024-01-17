using System.Text.RegularExpressions;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.Extensions;

public static class ProductExtensions
{
    public static IEnumerable<Product> HighlightWords(this IEnumerable<Product> products, string highlight)
    {
        if (!string.IsNullOrEmpty(highlight))
        {
            var highlightWords = highlight.Split(',').Select(w => w.Trim()).ToList();
            foreach (var product in products)
            {
                foreach (var word in highlightWords)
                {
                    product.Description = Regex.Replace(
                        product.Description,
                        $@"\b({Regex.Escape(word)}|{Regex.Escape(word.ToLowerInvariant())})\b",
                        match => $"<em>{match.Value}</em>",
                        RegexOptions.IgnoreCase
                    );
                }
            }
        }
        return products;
    }

    public static IEnumerable<Product> FilterBySize(this IEnumerable<Product> products, string size)
    {
        if (string.IsNullOrEmpty(size))
        {
            return products;
        }

        return products.Where(p => p.Sizes.Any(s => string.Equals(s, size, StringComparison.OrdinalIgnoreCase)));
    }

    public static IEnumerable<Product> FilterByMinPrice(this IEnumerable<Product> products, decimal? minPrice)
    {
        if (minPrice.HasValue)
        {
            return products.Where(p => p.Price >= minPrice.Value);
        }
        return products;
    }

    public static IEnumerable<Product> FilterByMaxPrice(this IEnumerable<Product> products, decimal? maxPrice)
    {
        if (maxPrice.HasValue)
        {
            return products.Where(p => p.Price <= maxPrice.Value);
        }
        return products;
    }

    public static List<string> GetWordOccurrences(this IEnumerable<Product> products)
    {
        var allDescriptions = products.Select(p => p.Description).ToList();

        return allDescriptions
            .SelectMany(desc => desc.Split(new[] { ' ', '.', ',' }, StringSplitOptions.RemoveEmptyEntries))
            .GroupBy(word => word.ToLower())
            .Select(group => new { Word = group.Key, Count = group.Count() })
            .OrderByDescending(x => x.Count)
            .Select(x => x.Word)
            .ToList();
    }
}
