using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Exceptions;

namespace Warehouse.Application.Features.Commands.Order.OrderDelete;

public class OrderDeleteCommandHandler : IRequestHandler<OrderDeleteCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderDeleteCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task Handle(OrderDeleteCommand command, CancellationToken cancellationToken)
    {
        var existingProduct = await _unitOfWork.Orders.GetById(command.orderId);
        if (existingProduct is null)
        {
            throw new ProductNotFoundException($"Order with ID {command.orderId} not found.");
        }

        existingProduct.IsDeleted = true;
        _unitOfWork.Orders.Update(existingProduct);
        await _unitOfWork.SaveAsync();
    }
}