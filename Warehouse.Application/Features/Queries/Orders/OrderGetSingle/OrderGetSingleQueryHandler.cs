using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto.OrderDtos;

namespace Warehouse.Application.Features.Queries.Orders.OrderGetSingle;

public class OrderGetSingleQueryHandler : IRequestHandler<OrderGetSingleQuery, OrderWithDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public OrderGetSingleQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<OrderWithDetailsDto> Handle(OrderGetSingleQuery query, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetSingleOrderAsync(query.OrderId);

        var mappedOrder = _mapper.Map<OrderWithDetailsDto>(order);

        return mappedOrder;
    }
}