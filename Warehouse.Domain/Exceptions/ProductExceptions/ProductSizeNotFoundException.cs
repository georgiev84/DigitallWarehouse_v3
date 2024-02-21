namespace Warehouse.Domain.Exceptions.ProductExceptions;

public class ProductSizeNotFoundException : Exception
{
    public ProductSizeNotFoundException(string message)
    : base(message)
    {
    }
}