using Warehouse.Application.Features.Commands.Product.ProductCreate;
using Warehouse.Application.Features.Commands.Product.Update;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Common.Interfaces.Factories;
public interface IProductFactory
{
    Product CreateProduct(ProductCreateCommand command);
    Product UpdateProductAsync(Product existingProduct, ProductUpdateCommand command);
}
