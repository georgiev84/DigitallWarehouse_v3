using Warehouse.Application.Features.Commands.Product.ProductCreate;
using Warehouse.Application.Features.Commands.Product.Update;
using Warehouse.Application.Models.Dto;

namespace Warehouse.Application.Common.Interfaces;

public interface IProductService
{
    Task<ProductDto> GetFilteredProductsAsync(ItemsDto items);
    Task<CreateProductDetailsDto> CreateProductAsync(ProductCreateCommand command);
    Task<UpdateProductDetailsDto> UpdateProductAsync(UpdateProductCommand command);
    Task DeleteProductAsync(Guid id);
}
