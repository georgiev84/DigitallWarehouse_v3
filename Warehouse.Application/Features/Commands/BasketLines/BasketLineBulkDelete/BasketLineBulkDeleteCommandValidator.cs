using FluentValidation;

namespace Warehouse.Application.Features.Commands.BasketLines.BasketLineBulkDelete;

public class BasketLineBulkDeleteCommandValidator : AbstractValidator<BasketLineBulkDeleteCommand>
{
    public BasketLineBulkDeleteCommandValidator()
    {
        RuleFor(command => command.UserId).NotEmpty();
    }
}