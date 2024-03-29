﻿using FluentValidation;
using Warehouse.Application.Common.Validation;

namespace Warehouse.Application.Features.Commands.Orders.OrderDelete;

public class OrderDeleteCommandValidator : AbstractValidator<OrderDeleteCommand>
{
    public OrderDeleteCommandValidator()
    {
        RuleFor(command => command.orderId).NotEmpty().WithMessage(ValidationMessages.RequiredId(nameof(OrderDeleteCommand.orderId)));
    }
}