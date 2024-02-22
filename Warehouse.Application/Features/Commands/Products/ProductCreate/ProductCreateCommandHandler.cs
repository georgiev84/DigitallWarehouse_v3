using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto.ProductDtos;
using Warehouse.Domain.Entities.Products;

namespace Warehouse.Application.Features.Commands.Products.ProductCreate;

public class ProductCreateCommandHandler(IMapper _mapper, IUnitOfWork _unitOfWork) : IRequestHandler<ProductCreateCommand, ProductCreateDetailsDto>
{
    public async Task<ProductCreateDetailsDto> Handle(ProductCreateCommand command, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(command);

        await _unitOfWork.Products.Add(product);
        await _unitOfWork.SaveAsync();

        var checkedProduct = await _unitOfWork.Products.GetProductDetailsByIdAsync(product.Id);

        var productDto = _mapper.Map<ProductCreateDetailsDto>(checkedProduct);

        return productDto;
    }
}