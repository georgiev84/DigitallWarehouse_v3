using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Features.Commands.Order.OrderCreate;
public class OrderCreateCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public OrderCreateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateOrderDto> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        //var order = _mapper.Map<CreateOrderCommand>(command);
        //// use factory here
        //var result = await _unitOfWork.Orders.Add(order);
        //var mappedResult = _mapper.Map<CreateOrderDto>(result);
        //return mappedResult;
        throw new NotImplementedException();
    }
}
