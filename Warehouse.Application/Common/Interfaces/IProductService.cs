using Warehouse.Application.Models.Dto;

namespace Warehouse.Application.Common.Interfaces;

public interface IProductService
{
    Task<ProductDto> GetFilteredProductsAsync(decimal? minPrice, decimal? maxPrice, string? size, string? highlight);
}
