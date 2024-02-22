using FluentValidation;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Common.Validation;

namespace Warehouse.Application.Features.Commands.BasketLines.BasketLineCreate;

public class BasketLineCreateCommandValidator : AbstractValidator<BasketLineCreateCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public BasketLineCreateCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(command => command.UserId)
            .NotEmpty().WithMessage(ValidationMessages.RequiredId(nameof(BasketLineCreateCommand.UserId)));

        RuleFor(command => command.BasketLine)
            .NotNull().WithMessage(ValidationMessages.RequiredItem("BasketLine"))
            .SetValidator(new BasketLineDtoValidator(_unitOfWork)!);
        
    }
}