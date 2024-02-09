namespace Warehouse.Domain.Exceptions;
public class BasketLineNotFoundException : Exception
{
    public BasketLineNotFoundException(string message)
     : base(message)
    {
    }
}