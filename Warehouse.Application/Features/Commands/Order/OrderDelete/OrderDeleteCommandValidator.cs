using FluentValidation;
using Warehouse.Application.Common.Validation;
using Warehouse.Application.Models.Dto.BasketDtos;

namespace Warehouse.Application.Features.Commands.Order.OrderDelete;

public class OrderDeleteCommandValidator : AbstractValidator<OrderDeleteCommand>
{
    public OrderDeleteCommandValidator()
    {
        RuleFor(command => command.orderId).NotEmpty().WithMessage(ValidationMessages.RequiredId(nameof(OrderDeleteCommand.orderId)));
    }
}