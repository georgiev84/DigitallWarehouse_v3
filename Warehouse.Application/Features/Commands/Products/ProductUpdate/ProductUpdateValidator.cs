﻿using FluentValidation;
using Warehouse.Application.Common.Validation;

namespace Warehouse.Application.Features.Commands.Products.Update;

public class ProductUpdateValidator : AbstractValidator<ProductUpdateCommand>
{
    public ProductUpdateValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage(ValidationMessages.RequiredId(nameof(ProductUpdateCommand.Id)));
    }
}