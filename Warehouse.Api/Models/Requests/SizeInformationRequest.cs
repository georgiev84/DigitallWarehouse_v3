namespace Warehouse.Api.Models.Requests;

public class SizeInformationRequest
{
    public Guid SizeId { get; set; }
    public int Quantity { get; set; }
}