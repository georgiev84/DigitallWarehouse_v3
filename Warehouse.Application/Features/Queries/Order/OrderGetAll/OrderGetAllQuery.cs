using MediatR;
using Warehouse.Application.Models.Dto.OrderDtos;

namespace Warehouse.Application.Features.Queries.Order.OrderGetAll;
public record OrderGetAllQuery() : IRequest<IEnumerable<OrderDto>>;