using MediatR;
using Warehouse.Application.Models.Dto.BasketDtos;

namespace Warehouse.Application.Features.Queries.Basket.BasketSingleQuery;
public record BasketSingleQuery(Guid UserId) : IRequest<BasketDetailDto>
{
    public BasketSingleQuery() : this(Guid.Empty)
    {
    }
}