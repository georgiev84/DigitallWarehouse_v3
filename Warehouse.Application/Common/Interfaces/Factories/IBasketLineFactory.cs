using Warehouse.Application.Features.Commands.BasketLine.BasketLineCreate;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Common.Interfaces.Factories;
public interface IBasketLineFactory
{
    BasketLine CreateBasketLine(BasketLineCreateCommand command);
}
