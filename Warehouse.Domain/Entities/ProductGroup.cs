using System.ComponentModel.DataAnnotations;

namespace Warehouse.Domain.Entities;
public class ProductGroup
{
    public Guid ProductGroupId { get; set; }

    public string GroupName { get; set; }

    public ICollection<Product> Products { get; set; }
}
