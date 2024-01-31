using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces;
using Warehouse.Application.Models.Dto;

namespace Warehouse.Application.Features.Commands.Product.ProductCreate;
public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, CreateProductDetailsDto>
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductCreateCommandHandler(IProductService productService, IMapper mapper)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<CreateProductDetailsDto> Handle(ProductCreateCommand command, CancellationToken cancellationToken)
    {
        var result = await _productService.CreateProductAsync(command);

        var items = _mapper.Map<CreateProductDetailsDto>(result);

        return items;
    }
}
