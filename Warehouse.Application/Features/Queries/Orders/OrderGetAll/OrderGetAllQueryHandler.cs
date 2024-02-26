using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto.OrderDtos;

namespace Warehouse.Application.Features.Queries.Orders.OrderGetAll;

public class OrderGetAllQueryHandler(IMapper _mapper, IUnitOfWork _unitOfWork) : IRequestHandler<OrderGetAllQuery, IEnumerable<OrderDto>>
{
    public async Task<IEnumerable<OrderDto>> Handle(OrderGetAllQuery query, CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.Orders.GetOrdersListAsync();
        var mappedOrders = _mapper.Map<IEnumerable<OrderDto>>(orders);
        return mappedOrders;
    }
}