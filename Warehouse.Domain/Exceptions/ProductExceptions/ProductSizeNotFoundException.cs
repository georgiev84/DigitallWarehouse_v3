namespace Warehouse.Domain.Exceptions.ProductExceptions;

public class ProductSizeNotFoundException : Exception
{
    public ProductSizeNotFoundException(string message)
    : base(message)
    {
    }

    public ProductSizeNotFoundException(string message, Exception exception)
    : base(message, exception)
    {
    }
}