using System.ComponentModel.DataAnnotations;

namespace Warehouse.Domain.Entities;
public class Order
{
    public Guid OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }

    public ICollection<OrderDetails> OrderDetails { get; set; }

    public Payment PaymentDetails { get; set; }
}
