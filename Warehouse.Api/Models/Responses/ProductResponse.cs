using Warehouse.Application.Models.Dto;
using Warehouse.Domain.Entities;

namespace Warehouse.Api.Models.Responses;

public class ProductResponse
{
    public ProductFilter? Filter { get; set; }
    public IEnumerable<Product>? Products { get; set; }
}
