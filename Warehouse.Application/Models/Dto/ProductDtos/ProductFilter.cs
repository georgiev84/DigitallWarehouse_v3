namespace Warehouse.Application.Models.Dto.ProductDtos;

public class ProductFilter
{
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public IEnumerable<string>? AllSizes { get; set; }
    public string[]? CommonWords { get; set; }
}