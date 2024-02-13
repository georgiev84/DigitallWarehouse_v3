using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto.OrderDtos;

namespace Warehouse.Application.Features.Queries.Order.OrderGetAll;

public class OrderGetAllQueryHandler : IRequestHandler<OrderGetAllQuery, IEnumerable<OrderDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public OrderGetAllQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<IEnumerable<OrderDto>> Handle(OrderGetAllQuery query, CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.Orders.GetOrdersListAsync();
        var mappedOrders = _mapper.Map<IEnumerable<OrderDto>>(orders);
        return mappedOrders;
    }
}