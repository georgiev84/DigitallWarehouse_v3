namespace Warehouse.Application.Models.Dto;
public class CreateOrderDto
{
    public Guid Id { get; set; }
    public string Status { get; set; }
    public DateTime OrderDate { get; set; }
    public string UserName { get; set; }
    public decimal TotalAmount { get; set; }
}
