using Warehouse.Domain.Entities;

namespace Warehouse.Domain.Responses;

public class ProductResponse
{
    public ProductFilter Filter { get; set; }
    public IEnumerable<Product> Products { get; set; }
   
}
