using MediatR;
using Warehouse.Application.Models.Dto.OrderDtos;

namespace Warehouse.Application.Features.Queries.Orders.OrderGetSingle;
public record OrderGetSingleQuery(Guid OrderId) : IRequest<OrderWithDetailsDto>;