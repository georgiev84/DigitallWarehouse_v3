using Warehouse.Domain.Entities;
using Warehouse.Domain.Responses;

namespace Warehouse.Api.Dto;

public class ProductDto
{
    public ProductFilter? Filter { get; set; }
    public IEnumerable<Product>? Products { get; set; }
}
