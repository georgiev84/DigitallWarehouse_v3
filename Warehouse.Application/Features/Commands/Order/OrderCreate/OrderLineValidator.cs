using FluentValidation;
using Warehouse.Application.Common.Validation;
using Warehouse.Domain.Entities.Orders;

namespace Warehouse.Application.Features.Commands.Order.OrderCreate;

public class OrderLineValidator : AbstractValidator<OrderLine>
{
    public OrderLineValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty().WithMessage(ValidationMessages.RequiredId(nameof(OrderLine.OrderId)));
        RuleFor(x => x.ProductId).NotEmpty().WithMessage(ValidationMessages.RequiredId(nameof(OrderLine.ProductId)));
        RuleFor(x => x.SizeId).NotEmpty().WithMessage(ValidationMessages.RequiredId(nameof(OrderLine.SizeId)));
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage(ValidationMessages.ItemBiggerThanZero(nameof(OrderLine.Quantity)));
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.ItemBiggerOrEqualToZero(nameof(OrderLine.Price)));
    }
}