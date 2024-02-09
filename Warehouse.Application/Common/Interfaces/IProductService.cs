using Warehouse.Application.Features.Commands.Product.ProductCreate;
using Warehouse.Application.Features.Commands.Product.Update;
using Warehouse.Application.Models.Dto.ProductDtos;

namespace Warehouse.Application.Common.Interfaces;

public interface IProductService
{
    Task<UpdateProductDetailsDto> UpdateProductAsync(ProductUpdateCommand command);
    Task DeleteProductAsync(Guid id);
}
