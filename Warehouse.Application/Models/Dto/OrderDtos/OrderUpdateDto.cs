namespace Warehouse.Application.Models.Dto.OrderDtos;
public class OrderUpdateDto
{
    public Guid Id { get; set; }
    public Guid StatusId { get; set; }
    public Guid PaymentId { get; set; }
    public DateTime OrderDate { get; set; }
    public Guid UserId { get; set; }
    public string? Status { get; set; }

    public string? FullName { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderLineDto>? OrderLines { get; set; }
}
