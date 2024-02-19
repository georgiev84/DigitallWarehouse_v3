using FluentValidation;
using Warehouse.Application.Common.Validation;
using Warehouse.Application.Models.Dto.BasketDtos;

namespace Warehouse.Application.Features.Commands.BasketLine.BasketLineCreate;

public class BasketLineDtoValidator : AbstractValidator<BasketLineDto>
{
    public BasketLineDtoValidator()
    {
        RuleFor(dto => dto.BasketId)
            .NotEmpty().WithMessage(ValidationMessages.RequiredId(nameof(BasketLineDto.BasketId)));
        RuleFor(dto => dto.ProductId)
            .NotEmpty().WithMessage(ValidationMessages.RequiredId(nameof(BasketLineDto.ProductId)));

        RuleFor(dto => dto.SizeId)
            .NotEmpty().WithMessage(ValidationMessages.RequiredId(nameof(BasketLineDto.SizeId)));

        RuleFor(dto => dto.Quantity)
            .GreaterThan(0).WithMessage(ValidationMessages.ItemBiggerThanZero(nameof(BasketLineDto.Quantity)));

        RuleFor(dto => dto.Price)
            .GreaterThan(0).WithMessage(ValidationMessages.ItemBiggerThanZero(nameof(BasketLineDto.Price)));
    }
}