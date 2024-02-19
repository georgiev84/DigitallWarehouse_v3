namespace Warehouse.Domain.Exceptions.BasketExceptions;

public class BasketLineExistException : Exception
{
    public BasketLineExistException(string message) : base(message)
    {
    }
}