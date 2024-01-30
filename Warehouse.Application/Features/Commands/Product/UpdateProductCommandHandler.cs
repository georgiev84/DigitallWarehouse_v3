using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces;
using Warehouse.Application.Models.Dto;

namespace Warehouse.Application.Features.Commands.Product;
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductDetailsDto>
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IProductService productService, IMapper mapper)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<UpdateProductDetailsDto> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var result = await _productService.UpdateProductAsync(command);

        var item = _mapper.Map<UpdateProductDetailsDto>(result);

        return item;
    }
}
