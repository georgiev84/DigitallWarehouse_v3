namespace Warehouse.Domain.Exceptions.BasketExceptions;

public class BasketNotFoundException : Exception
{
    public BasketNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public BasketNotFoundException(string message) : base(message)
    {
    }
}