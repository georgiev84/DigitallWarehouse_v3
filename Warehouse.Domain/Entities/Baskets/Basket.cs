using Warehouse.Domain.Entities.Users;

namespace Warehouse.Domain.Entities.Baskets;

public class Basket
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public ICollection<BasketLine> BasketLines { get; set; }
}