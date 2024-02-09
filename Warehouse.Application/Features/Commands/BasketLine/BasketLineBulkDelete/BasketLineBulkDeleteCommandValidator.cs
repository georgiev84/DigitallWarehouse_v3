using FluentValidation;
using Warehouse.Application.Features.Commands.BasketLine.BasketLineDelete;

namespace Warehouse.Application.Features.Commands.BasketLine.BasketLineBulkDelete;

public class BasketLineBulkDeleteCommandValidator : AbstractValidator<BasketLineBulkDeleteCommand>
{
    public BasketLineBulkDeleteCommandValidator()
    {
        RuleFor(command => command.UserId).NotEmpty();
    }
}
