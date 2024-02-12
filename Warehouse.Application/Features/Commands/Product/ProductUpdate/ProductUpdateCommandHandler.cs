using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto.ProductDtos;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Exceptions;

namespace Warehouse.Application.Features.Commands.Product.Update;
public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, ProductUpdateDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ProductUpdateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductUpdateDetailsDto> Handle(ProductUpdateCommand command, CancellationToken cancellationToken)
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
}
