using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Exceptions;

namespace Warehouse.Application.Features.Commands.Order.OrderUpdate;
public class OrderUpdateCommandHandler : IRequestHandler<OrderUpdateCommand, OrderUpdateDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<OrderUpdateDto> Handle(OrderUpdateCommand command, CancellationToken cancellationToken)
    {
        var existingOrder = await _unitOfWork.Orders.GetSingleOrderAsync(command.Id);
        if (existingOrder == null)
        {
            throw new ProductNotFoundException($"Order with ID {command.Id} not found.");
        }

        existingOrder.StatusId = command.StatusId;
        existingOrder.PaymentId = command.PaymentId;
        existingOrder.OrderDate = command.OrderDate;
        existingOrder.UserId = command.UserId;
        existingOrder.TotalAmount = command.TotalAmount;

        // Remove order lines that are not present in the updated order
        var orderLinesToRemove = existingOrder.OrderLines.Where(ol => !command.OrderLines.Any(ud => ud.ProductId == ol.ProductId));
        foreach (var orderLineToRemove in orderLinesToRemove.ToList())
        {
            existingOrder.OrderLines.Remove(orderLineToRemove);
        }

        // Update or add order lines from the command
        foreach (var updatedOrderLine in command.OrderLines)
        {
            var existingOrderLine = existingOrder.OrderLines.FirstOrDefault(ol => ol.ProductId == updatedOrderLine.ProductId);
            if (existingOrderLine != null)
            {
                // Update existing order line
                existingOrderLine.SizeId = updatedOrderLine.SizeId;
                existingOrderLine.Quantity = updatedOrderLine.Quantity;
                existingOrderLine.Price = updatedOrderLine.Price;
            }
            else
            {
                // Add new order line
                existingOrder.OrderLines.Add(new OrderLine
                {
                    ProductId = updatedOrderLine.ProductId,
                    SizeId = updatedOrderLine.SizeId,
                    Quantity = updatedOrderLine.Quantity,
                    Price = updatedOrderLine.Price
                });
            }
        }


        _unitOfWork.Orders.Update(existingOrder);
        _unitOfWork.Save();

        var updatedOrderDto = _mapper.Map<OrderUpdateDto>(existingOrder);
        return updatedOrderDto;
    }
}
