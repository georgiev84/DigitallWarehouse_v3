using Warehouse.Domain.Entities;

namespace Warehouse.Application.Common.Interfaces.Persistence;
public interface ISizeRepository : IGenericRepository<Size>
{
    Task<IEnumerable<string>> GetSizeNamesAsync();
}
