using FluentValidation;
using Warehouse.Application.Common.Validation;
using Warehouse.Application.Features.Commands.Product.ProductCreate;

namespace Warehouse.Application.Features.Commands.Order.OrderUpdate;

public class OrderUpdateValidator : AbstractValidator<OrderUpdateCommand>
{
    public OrderUpdateValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage(ValidationMessages.RequiredId(nameof(OrderUpdateCommand.Id)));
    }
}