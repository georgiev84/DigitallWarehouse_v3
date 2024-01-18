using Warehouse.Application.Models.Dto;

namespace Warehouse.Application.Common.Interfaces;

public interface IProductService
{
    Task<ProductDto> GetFilteredProductsAsync(ItemsDto items);
}
