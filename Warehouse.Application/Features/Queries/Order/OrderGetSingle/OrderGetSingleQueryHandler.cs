using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto;

namespace Warehouse.Application.Features.Queries.Order.OrderGetSingle;
public class OrderGetSingleQueryHandler : IRequestHandler<OrderGetSingleQuery, OrderDto>
{
    private readonly ILogger<OrderGetSingleQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public OrderGetSingleQueryHandler(ILogger<OrderGetSingleQueryHandler> logger, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<OrderDto> Handle(OrderGetSingleQuery query, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetSingleOrderAsync(query.OrderId);
        var mappedOrder = _mapper.Map<OrderDto>(order);
        return mappedOrder;
    }
}
