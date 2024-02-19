namespace Warehouse.Domain.Exceptions.OrderExceptions;

public class OrderNotFoundException : Exception
{
    public OrderNotFoundException(string message) : base(message)
    {
    }
}