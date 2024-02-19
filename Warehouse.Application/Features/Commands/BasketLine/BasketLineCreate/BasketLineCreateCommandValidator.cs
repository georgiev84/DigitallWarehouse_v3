using FluentValidation;
using Warehouse.Application.Common.Validation;

namespace Warehouse.Application.Features.Commands.BasketLine.BasketLineCreate;

public class BasketLineCreateCommandValidator : AbstractValidator<BasketLineCreateCommand>
{
    public BasketLineCreateCommandValidator()
    {
        RuleFor(command => command.UserId)
            .NotEmpty().WithMessage(ValidationMessages.RequiredId(nameof(BasketLineCreateCommand.UserId)));

        RuleFor(command => command.BasketLine)
            .NotNull().WithMessage(ValidationMessages.RequiredItem("BasketLine"))
            .SetValidator(new BasketLineDtoValidator()!);
    }
}