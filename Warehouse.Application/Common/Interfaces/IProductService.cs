using Warehouse.Application.Features.Commands.Product;
using Warehouse.Application.Models.Dto;

namespace Warehouse.Application.Common.Interfaces;

public interface IProductService
{
    Task<ProductDto> GetFilteredProductsAsync(ItemsDto items);
    Task<CreateProductDetailsDto> CreateProductAsync(CreateProductCommand command);
    Task<UpdateProductDetailsDto> UpdateProductAsync(UpdateProductCommand command);
    Task DeleteProductAsync(Guid id);
}
