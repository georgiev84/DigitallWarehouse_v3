using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Exceptions;

namespace Warehouse.Application.Features.Commands.Products.Delete;

public class ProductDeleteCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<ProductDeleteCommand>
{
    public async Task Handle(ProductDeleteCommand command, CancellationToken cancellationToken)
    {
        var existingProduct = await _unitOfWork.Products.GetById(command.productId);
        if (existingProduct is null)
        {
            throw new ProductNotFoundException($"Product with ID {command.productId} not found.");
        }

        existingProduct.IsDeleted = true;
        _unitOfWork.Products.Update(existingProduct);
        await _unitOfWork.SaveAsync();
    }
}