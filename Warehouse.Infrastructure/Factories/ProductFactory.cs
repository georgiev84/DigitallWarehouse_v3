using Warehouse.Application.Common.Interfaces.Factories;
using Warehouse.Application.Features.Commands.Product.ProductCreate;
using Warehouse.Application.Features.Commands.Product.Update;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Exceptions;

namespace Warehouse.Infrastructure.Factories;
public class ProductFactory : IProductFactory
{
    public Product CreateProduct(ProductCreateCommand command)
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

    public Product UpdateProductAsync(Product existingProduct, ProductUpdateCommand command)
    {
        existingProduct.Title = command.Title;
        existingProduct.Description = command.Description;
        existingProduct.Price = command.Price;
        existingProduct.BrandId = command.BrandId;

        foreach (var newSize in command.SizeInformation)
        {
            var existingSize = existingProduct.ProductSizes.FirstOrDefault(ps => ps.SizeId == newSize.SizeId);
            if (existingSize != null)
            {
                existingSize.QuantityInStock = newSize.Quantity;
            }
            else
            {
                existingProduct.ProductSizes.Add(new ProductSize
                {
                    SizeId = newSize.SizeId,
                    QuantityInStock = newSize.Quantity
                });
            }
        }

        existingProduct.ProductGroups.Clear();
        foreach (var groupId in command.GroupIds)
        {
            existingProduct.ProductGroups.Add(new ProductGroup
            {
                GroupId = groupId
            });
        }

        return existingProduct;
    }
}
