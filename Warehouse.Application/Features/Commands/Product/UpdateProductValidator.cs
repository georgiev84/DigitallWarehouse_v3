﻿using FluentValidation;

namespace Warehouse.Application.Features.Commands.Product;
public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}