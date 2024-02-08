using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities;
using Warehouse.Infrastructure.Persistence.Contexts;

namespace Warehouse.Infrastructure.Persistence.Repositories;
public class BasketLineRepository : GenericRepository<BasketLine>, IBasketLineRepository
{
    public BasketLineRepository(WarehouseDbContext context) : base(context)
    {

    }

    public Task<BasketLine> GetBasketByUserId(Guid userId)
    {
        throw new NotImplementedException();
    }
}
