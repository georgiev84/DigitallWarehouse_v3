﻿using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;

namespace Warehouse.Application.Features.Commands.Orders.OrderDelete;

public class OrderDeleteCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<OrderDeleteCommand>
{
    public async Task Handle(OrderDeleteCommand command, CancellationToken cancellationToken)
    {
        var existingProduct = await _unitOfWork.Orders.GetById(command.orderId);

        existingProduct.IsDeleted = true;
        await _unitOfWork.Orders.Update(existingProduct);
        await _unitOfWork.SaveAsync();
    }
}