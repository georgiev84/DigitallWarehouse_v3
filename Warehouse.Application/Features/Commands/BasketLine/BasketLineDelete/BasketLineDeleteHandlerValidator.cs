using FluentValidation;

namespace Warehouse.Application.Features.Commands.BasketLine.BasketLineDelete;

public class BasketLineDeleteHandlerValidator : AbstractValidator<BasketLineDeleteCommand>
{
    public BasketLineDeleteHandlerValidator()
    {
        RuleFor(command => command.BasketLineId).NotEmpty();
    }
}