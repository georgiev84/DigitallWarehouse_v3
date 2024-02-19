namespace Warehouse.Domain.Exceptions.BasketExceptions;

public class BasketLineNotFoundException : Exception
{
    public BasketLineNotFoundException(string message)
     : base(message)
    {
    }
}