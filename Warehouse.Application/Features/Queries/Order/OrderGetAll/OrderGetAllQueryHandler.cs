using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto;

namespace Warehouse.Application.Features.Queries.Order.OrderGetAll;
public class OrderGetAllQueryHandler : IRequestHandler<OrderGetAllQuery, IEnumerable<OrderDto>>
{
    private readonly ILogger<OrderGetAllQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public OrderGetAllQueryHandler(ILogger<OrderGetAllQueryHandler> logger, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
