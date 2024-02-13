using FluentValidation;

namespace Warehouse.Application.Features.Commands.BasketLine.BasketLineCreate;

public class BasketLineCreateCommandValidator : AbstractValidator<BasketLineCreateCommand>
{
    public BasketLineCreateCommandValidator()
    {
        RuleFor(command => command.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(command => command.BasketLine)
            .NotNull().WithMessage("BasketLine is required.")
            .SetValidator(new BasketLineDtoValidator()!);
    }
}