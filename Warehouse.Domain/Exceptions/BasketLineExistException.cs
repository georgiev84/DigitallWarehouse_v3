namespace Warehouse.Domain.Exceptions;

public class BasketLineExistException : Exception
{
    public BasketLineExistException(string message) : base(message)
    {
    }
}