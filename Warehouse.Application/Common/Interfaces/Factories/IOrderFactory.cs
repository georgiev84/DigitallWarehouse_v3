using Warehouse.Application.Features.Commands.Order.OrderCreate;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Common.Interfaces.Factories;

public interface IOrderFactory
{
    Order CreateOrder(OrderCreateCommand command);
}