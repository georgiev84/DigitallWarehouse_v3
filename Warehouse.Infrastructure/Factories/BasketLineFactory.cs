using Warehouse.Application.Common.Interfaces.Factories;
using Warehouse.Application.Features.Commands.BasketLine.BasketLineCreate;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.Factories;
public class BasketLineFactory : IBasketLineFactory
{
    public BasketLine CreateBasketLine(BasketLineCreateCommand command)
    {
        BasketLine basketLine = new BasketLine
        {
            ProductId = command.BasketLine.ProductId,
            SizeId = command.BasketLine.SizeId,
            Quantity = command.BasketLine.Quantity,
            Price = command.BasketLine.Price
        };

        return basketLine;
    }
}
