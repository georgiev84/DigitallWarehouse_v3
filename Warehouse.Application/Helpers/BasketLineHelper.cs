using Warehouse.Application.Features.Commands.BasketLine.BasketLineCreate;
using Warehouse.Domain.Entities;

namespace Warehouse.Persistence.EF.Factories;

public static class BasketLineHelper
{
    public static BasketLine CreateBasketLine(BasketLineCreateCommand command)
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