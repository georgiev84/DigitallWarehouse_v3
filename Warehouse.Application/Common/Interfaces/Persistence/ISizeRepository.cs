using Warehouse.Domain.Entities;
using Warehouse.Persistence.Abstractions.Interfaces;

namespace Warehouse.Application.Common.Interfaces.Persistence;
public interface ISizeRepository : IGenericRepository<Size>
{
    Task<IEnumerable<string>> GetSizeNamesAsync();
}
