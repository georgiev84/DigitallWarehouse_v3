using Warehouse.Domain.Entities;
using Warehouse.Domain.Models;

namespace Warehouse.Application.Models.Dto;

public class ProductDto
{
    public ProductFilter? Filter { get; set; }
    public IEnumerable<ProductDetailsDto>? Products { get; set; }
}
