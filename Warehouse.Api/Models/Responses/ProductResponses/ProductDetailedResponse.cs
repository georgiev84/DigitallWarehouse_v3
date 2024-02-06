using Warehouse.Application.Models.Dto;

namespace Warehouse.Api.Models.Responses.ProductResponses;

public class ProductDetailedResponse
{
    public ProductFilter? Filter { get; set; }
    public IEnumerable<ProductDetailsDto>? Products { get; set; }
}
