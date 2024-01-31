using MediatR;
using Warehouse.Application.Models.Dto;

namespace Warehouse.Application.Features.Queries.Order.OrderGetSingle;
public record OrderGetSingleQuery(Guid OrderId) : IRequest<OrderDto>
{
    public OrderGetSingleQuery() : this(Guid.Empty)
    {
    }
}
