using Warehouse.Domain.Entities;
using Warehouse.Domain.Responses;

namespace Warehouse.Application.Common.Interfaces;

public interface IProductService
{
    Task<ProductResponse> GetFilteredProductsAsync(decimal? minPrice, decimal? maxPrice, string size, string highlight);
}
