using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces.Factories;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto.OrderDtos;

namespace Warehouse.Application.Features.Commands.Order.OrderCreate;
public class OrderCreateCommandHandler : IRequestHandler<OrderCreateCommand, OrderCreateDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderFactory _orderFactory;

    public OrderCreateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IOrderFactory orderFactory)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _orderFactory = orderFactory ?? throw new ArgumentNullException(nameof(orderFactory));
    }

    public async Task<OrderCreateDto> Handle(OrderCreateCommand command, CancellationToken cancellationToken)
    {
        var order = _orderFactory.CreateOrder(command);

        await _unitOfWork.Orders.Add(order);
        await _unitOfWork.SaveAsync();

        var checkedOrder = await _unitOfWork.Orders.GetById(order.Id);

        var orderDto = _mapper.Map<OrderCreateDto>(checkedOrder);

        return orderDto;
    }
}
