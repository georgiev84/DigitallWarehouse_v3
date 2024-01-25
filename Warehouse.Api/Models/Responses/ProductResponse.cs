using Warehouse.Application.Models.Dto;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Models;

namespace Warehouse.Api.Models.Responses;

public class ProductResponse
{
    public ProductFilter? Filter { get; set; }
    public IEnumerable<ProductDetailsDto>? Products { get; set; }
}
