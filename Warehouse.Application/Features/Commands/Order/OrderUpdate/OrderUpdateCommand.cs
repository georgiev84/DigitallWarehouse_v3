﻿using MediatR;
using Warehouse.Application.Models.Dto.OrderDtos;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Features.Commands.Order.OrderUpdate;
public record OrderUpdateCommand(
    Guid Id, 
    Guid StatusId, 
    Guid PaymentId) : IRequest<OrderUpdateDto>;

