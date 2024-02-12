using AutoMapper;
using Microsoft.Extensions.Logging;
using Warehouse.Application.Common.Interfaces;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Features.Commands.Product.Update;
using Warehouse.Application.Models.Dto.ProductDtos;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Exceptions;


namespace Warehouse.Persistence.EF.Services;

public class ProductService : IProductService
{
    private readonly ILogger<ProductService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(ILogger<ProductService> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductUpdateDetailsDto> UpdateProductAsync(ProductUpdateCommand command)
    {
        var existingProduct = await _unitOfWork.Products.GetProductDetailsByIdAsync(command.Id);
        if (existingProduct == null)
        {
            throw new ProductNotFoundException($"Product with ID {command.Id} not found.");
        }

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

        _unitOfWork.SaveAsync();

        var updatedProductDto = _mapper.Map<ProductUpdateDetailsDto>(existingProduct);

        return updatedProductDto;
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var product = await _unitOfWork.Products.GetById(id);
        if (product == null)
        {
            throw new ProductNotFoundException($"Product with Id: {id} not found!");
        }

        _unitOfWork.Products.Delete(product);
        _unitOfWork.SaveAsync();
    }
}
