using Warehouse.Domain.Entities;

namespace Warehouse.Application.Models.Dto;

public class ProductDto
{
    public ProductFilter? Filter { get; set; }
    public IEnumerable<Product>? Products { get; set; }
}
