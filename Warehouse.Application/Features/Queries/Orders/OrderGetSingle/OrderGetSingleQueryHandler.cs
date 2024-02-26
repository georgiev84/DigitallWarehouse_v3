using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto.OrderDtos;

namespace Warehouse.Application.Features.Queries.Orders.OrderGetSingle;

public class OrderGetSingleQueryHandler(IMapper _mapper, IUnitOfWork _unitOfWork) : IRequestHandler<OrderGetSingleQuery, OrderWithDetailsDto>
{
    public async Task<OrderWithDetailsDto> Handle(OrderGetSingleQuery query, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetSingleOrderAsync(query.OrderId);

        var mappedOrder = _mapper.Map<OrderWithDetailsDto>(order);

        return mappedOrder;
    }
}