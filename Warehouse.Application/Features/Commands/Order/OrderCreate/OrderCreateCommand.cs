using MediatR;
using Warehouse.Application.Models.Dto;

namespace Warehouse.Application.Features.Commands.Order.OrderCreate;
public record CreateOrderCommand() : IRequest<CreateOrderDto>;