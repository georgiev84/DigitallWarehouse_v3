using Warehouse.Domain.Responses;

namespace Warehouse.Application.Common.Interfaces;

public interface IProductService
{
    Task<ProductDomainModel> GetFilteredProductsAsync(decimal? minPrice, decimal? maxPrice, string size, string highlight);
}
