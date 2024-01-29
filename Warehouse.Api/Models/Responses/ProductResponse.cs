using Warehouse.Application.Models.Dto;

namespace Warehouse.Api.Models.Responses;

public class ProductResponse
{
    public ProductFilter? Filter { get; set; }
    public IEnumerable<ProductDetailsDto>? Products { get; set; }
}
