namespace Warehouse.Domain.Exceptions.ProductExceptions;

public class ProductCreationException : Exception
{
    public ProductCreationException(string message) : base(message)
    {
    }
}