
namespace Warehouse.Application.Models.Dto;

public class ProductDto
{
    public ProductFilter? Filter { get; set; }
    public IEnumerable<ProductDetailsDto>? Products { get; set; }
}
