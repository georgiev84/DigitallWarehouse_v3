namespace Warehouse.Application.Models.Dto;
public class OrderWithDetailsDto
{
    public Guid Id { get; set; }
    public string Status { get; set; }
    public DateTime OrderDate { get; set; }
    public string FullName { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderLinesDto> OrderLines { get; set; }
}
