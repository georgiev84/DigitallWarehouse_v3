using FluentValidation;

namespace Warehouse.Application.Features.Commands.BasketLines.BasketLineDelete;

public class BasketLineDeleteHandlerValidator : AbstractValidator<BasketLineDeleteCommand>
{
    public BasketLineDeleteHandlerValidator()
    {
        RuleFor(command => command.BasketLineId).NotEmpty();
    }
}