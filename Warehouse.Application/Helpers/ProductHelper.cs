using Warehouse.Application.Features.Commands.Product.ProductCreate;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Helpers;

public static class ProductHelper
{
    public static Product CreateProduct(ProductCreateCommand command)
    {
        var product = new Product
        {
            Title = command.Title,
            Description = command.Description,
            Price = command.Price,
            BrandId = command.BrandId,
            ProductSizes = command.SizeInformation.Select(size => new ProductSize
            {
                SizeId = size.SizeId,
                QuantityInStock = size.Quantity
            }).ToList(),
            ProductGroups = command.GroupIds.Select(groupId => new ProductGroup
            {
                GroupId = groupId
            }).ToList()
        };

        return product;
    }
}