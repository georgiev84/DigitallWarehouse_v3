using MediatR;
using Warehouse.Application.Models.Dto.OrderDtos;

namespace Warehouse.Application.Features.Queries.Order.OrderGetSingle;
public record OrderGetSingleQuery(Guid OrderId) : IRequest<OrderWithDetailsDto>
{
    public OrderGetSingleQuery() : this(Guid.Empty)
    {
    }
}