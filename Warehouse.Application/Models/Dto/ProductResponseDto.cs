using Warehouse.Domain.Entities;
using Warehouse.Domain.Responses;

namespace Warehouse.Application.Models.Dto;

public class ProductResponseDto
{
    public ProductFilter Filter { get; set; }
    public IEnumerable<Product> Products { get; set; }
}
