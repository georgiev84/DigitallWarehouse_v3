using FluentValidation;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Common.Validation;
using Warehouse.Application.Models.Dto.BasketDtos;

namespace Warehouse.Application.Features.Commands.BasketLines.BasketLineCreate;

public class BasketLineDtoValidator : AbstractValidator<BasketLineDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public BasketLineDtoValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

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

        RuleFor(command => command.BasketId)
        .MustAsync(Exist).WithMessage(command => $"Basket line for BasketID {command.BasketId} not found.");
    }

    private async Task<bool> Exist(Guid basketId, CancellationToken cancellationToken)
    {
        var existingBasketLine = await _unitOfWork.BasketLines.GetById(basketId);
        return existingBasketLine != null;
    }
}