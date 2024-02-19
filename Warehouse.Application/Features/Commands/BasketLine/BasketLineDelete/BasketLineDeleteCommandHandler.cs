using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Exceptions.BasketExceptions;

namespace Warehouse.Application.Features.Commands.BasketLine.BasketLineDelete;

public class BasketLineDeleteCommandHandler : IRequestHandler<BasketLineDeleteCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public BasketLineDeleteCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

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