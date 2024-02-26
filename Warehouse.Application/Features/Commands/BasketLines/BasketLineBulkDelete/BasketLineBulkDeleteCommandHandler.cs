using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Exceptions.BasketExceptions;

namespace Warehouse.Application.Features.Commands.BasketLines.BasketLineBulkDelete;

public class BasketLineBulkDeleteCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<BasketLineBulkDeleteCommand>
{
    public async Task Handle(BasketLineBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var basket = await _unitOfWork.Baskets.GetSingleBasketByUserIdAsync(command.UserId);
        if (basket is null)
        {
            throw new BasketNotFoundException($"Basket for User with ID {command.UserId} not found.");
        }

        await _unitOfWork.BasketLines.BulkDelete(basket.Id);
        await _unitOfWork.SaveAsync();
    }
}