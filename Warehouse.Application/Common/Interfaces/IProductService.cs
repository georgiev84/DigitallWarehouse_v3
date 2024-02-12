using Warehouse.Application.Features.Commands.Product.ProductCreate;
using Warehouse.Application.Features.Commands.Product.Update;
using Warehouse.Application.Models.Dto.ProductDtos;

namespace Warehouse.Application.Common.Interfaces;

public interface IProductService
{
    Task<ProductUpdateDetailsDto> UpdateProductAsync(ProductUpdateCommand command);
    Task DeleteProductAsync(Guid id);
}
