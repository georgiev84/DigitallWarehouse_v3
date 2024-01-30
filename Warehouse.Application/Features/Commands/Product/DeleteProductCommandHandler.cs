using MediatR;
using Warehouse.Application.Common.Interfaces;
using Warehouse.Application.Models.Dto;

namespace Warehouse.Application.Features.Commands.Product;
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductService _productService;

    public DeleteProductCommandHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        await _productService.DeleteProductAsync(command.productId);
    }
}
