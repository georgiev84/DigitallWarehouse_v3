using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Exceptions;

namespace Warehouse.Application.Features.Commands.Products.Delete;

public class ProductDeleteCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<ProductDeleteCommand>
{
    public async Task Handle(ProductDeleteCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var existingProduct = await _unitOfWork.Products.GetById(command.productId);
            if (existingProduct is null)
            {
                throw new ProductNotFoundException($"Product with ID {command.productId} not found.");
            }

            _unitOfWork.Products.Delete(existingProduct);
            _unitOfWork.Commit();
        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();
            throw new ApplicationException("An error occurred while processing the request.", ex);
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }
}