using Warehouse.Application.Features.Commands.Order.OrderCreate;
using Warehouse.Domain.Entities;

namespace Warehouse.Persistence.EF.Factories;

public static class OrderHelper
{
    public static Order CreateOrder(OrderCreateCommand command)
    {
        Order order = new Order
        {
            StatusId = command.StatusId,
            PaymentId = command.PaymentId,
            OrderDate = command.OrderDate,
            UserId = command.UserId,
            TotalAmount = command.TotalAmount,
            OrderLines = command.OrderLines
        };

        return order;
    }
}