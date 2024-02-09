using FluentValidation;
using Warehouse.Application.Models.Dto.BasketDtos;

namespace Warehouse.Application.Features.Commands.BasketLine.BasketLineCreate;
public class BasketLineDtoValidator : AbstractValidator<BasketLineDto>
{
    public BasketLineDtoValidator()
    {
        RuleFor(dto => dto.BasketId)
            .NotEmpty().WithMessage("BasketId is required.");

        RuleFor(dto => dto.ProductId)
            .NotEmpty().WithMessage("ProductId is required.");

        RuleFor(dto => dto.SizeId)
            .NotEmpty().WithMessage("SizeId is required.");

        RuleFor(dto => dto.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

        RuleFor(dto => dto.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");
    }
}
