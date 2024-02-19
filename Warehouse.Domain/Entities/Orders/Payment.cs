using Warehouse.Domain.Enums;

namespace Warehouse.Domain.Entities.Orders;

public class Payment
{
    public Guid PaymentId { get; set; }

    public PaymentMethod PaymentMethod { get; set; }

    public DateTime PaymentDate { get; set; }

    public Order Order { get; set; }
}