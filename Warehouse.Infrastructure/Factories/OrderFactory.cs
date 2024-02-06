using Warehouse.Application.Common.Interfaces.Factories;
using Warehouse.Application.Features.Commands.Order.OrderCreate;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.Factories;
public class OrderFactory : IOrderFactory
{
    public Order CreateOrder(OrderCreateCommand command)
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
