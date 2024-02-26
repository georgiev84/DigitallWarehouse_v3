using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Exceptions.BasketExceptions;

namespace Warehouse.Application.Features.Commands.BasketLines.BasketLineDelete;

public class BasketLineDeleteCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<BasketLineDeleteCommand>
{
    public async Task Handle(BasketLineDeleteCommand command, CancellationToken cancellationToken)
    {
        var basketLine = await _unitOfWork.BasketLines.GetById(command.BasketLineId);

        if (basketLine is null)
        {
            throw new BasketLineNotFoundException($"BasketLine with ID {command.BasketLineId} not found.");
        }

        _unitOfWork.BasketLines.Delete(basketLine);
        await _unitOfWork.SaveAsync();
    }
}