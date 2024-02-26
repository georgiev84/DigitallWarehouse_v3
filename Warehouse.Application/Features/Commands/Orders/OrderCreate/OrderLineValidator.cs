using FluentValidation;
using Warehouse.Application.Common.Validation;
using Warehouse.Application.Models.Dto.OrderDtos;

namespace Warehouse.Application.Features.Commands.Orders.OrderCreate;

public class OrderLineValidator : AbstractValidator<OrderLineDto>
{
    public OrderLineValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty().WithMessage(ValidationMessages.RequiredId(nameof(OrderLineDto.OrderId)));
        RuleFor(x => x.ProductId).NotEmpty().WithMessage(ValidationMessages.RequiredId(nameof(OrderLineDto.ProductId)));
        RuleFor(x => x.SizeId).NotEmpty().WithMessage(ValidationMessages.RequiredId(nameof(OrderLineDto.SizeId)));
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage(ValidationMessages.ItemBiggerThanZero(nameof(OrderLineDto.Quantity)));
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.ItemBiggerOrEqualToZero(nameof(OrderLineDto.Price)));
    }
}