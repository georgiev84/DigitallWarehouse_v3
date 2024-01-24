namespace Warehouse.Application.Common.Interfaces.Persistence;
public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    ISizeRepository Sizes { get; }
    int Save();
}
