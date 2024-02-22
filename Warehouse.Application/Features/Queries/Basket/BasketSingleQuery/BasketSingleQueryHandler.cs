using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto.BasketDtos;

namespace Warehouse.Application.Features.Queries.Basket.BasketSingleQuery;

public class BasketSingleQueryHandler(IMapper _mapper, IUnitOfWork _unitOfWork) : IRequestHandler<BasketSingleQuery, BasketDetailDto>
{
    public async Task<BasketDetailDto> Handle(BasketSingleQuery query, CancellationToken cancellationToken)
    {
        var basket = await _unitOfWork.Baskets.GetSingleBasketWithDetailsByUserIdAsync(query.UserId);

        var mappedBasket = _mapper.Map<BasketDetailDto>(basket);

        return mappedBasket;
    }
}