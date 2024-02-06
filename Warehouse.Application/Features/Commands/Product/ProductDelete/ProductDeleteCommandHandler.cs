using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Exceptions;

namespace Warehouse.Application.Features.Commands.Product.Delete;
public class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductDeleteCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ProductDeleteCommand command, CancellationToken cancellationToken)
    {
        var existingProduct = await _unitOfWork.Products.GetById(command.productId);
        if (existingProduct == null)
        {
            throw new ProductNotFoundException($"Product with ID {command.productId} not found.");
        }

        existingProduct.IsDeleted = true;
        _unitOfWork.Products.Update(existingProduct);
        _unitOfWork.Save();
    }
}
