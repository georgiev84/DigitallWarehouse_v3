using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto.BasketDtos;

namespace Warehouse.Application.Features.Queries.Basket.BasketSingleQuery;
public class BasketSingleQueryHandler : IRequestHandler<BasketSingleQuery, BasketDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public BasketSingleQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<BasketDetailDto> Handle(BasketSingleQuery query, CancellationToken cancellationToken)
    {
        var basket = await _unitOfWork.Baskets.GetSingleBasketWithDetailsByUserIdAsync(query.UserId);

        var mappedBasket = _mapper.Map<BasketDetailDto>(basket);

        return mappedBasket;
    }
}
