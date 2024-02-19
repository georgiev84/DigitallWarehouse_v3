using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Exceptions;

namespace Warehouse.Application.Features.Commands.BasketLine.BasketLineBulkDelete;

public class BasketLineBulkDeleteCommandHandler : IRequestHandler<BasketLineBulkDeleteCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public BasketLineBulkDeleteCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

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