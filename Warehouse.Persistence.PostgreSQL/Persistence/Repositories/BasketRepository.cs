using Dapper;
using System.Data;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities.Baskets;
using Warehouse.Domain.Entities.Products;
using Warehouse.Domain.Entities.Users;
using Warehouse.Domain.Exceptions.BasketExceptions;
using Warehouse.Persistence.Abstractions;
using Warehouse.Persistence.PostgreSQL.Configuration.Contstants;
using Warehouse.Persistence.PostgreSQL.Persistence.Contexts;

namespace Warehouse.Persistence.PostgreSQL.Persistence.Repositories;

public class BasketRepository : GenericRepository<Basket>, IBasketRepository
{
    public BasketRepository(WarehouseDbContext dbContext, IDbConnection dbConnection) : base(dbContext, dbConnection)
    {
    }

    public async Task<Basket> GetSingleBasketByUserIdAsync(Guid userId)
    {
        try
        {
            var basketDictionary = new Dictionary<Guid, Basket>();
            var basketLines = await _dbConnection.QueryAsync<Basket, BasketLine, Basket>(
                DapperConstants.GetSingleBasketQuery,
                (basket, basketLine) =>
                {
                    Basket basketEntry;
                    if (!basketDictionary.TryGetValue(basket.Id, out basketEntry))
                    {
                        basketDictionary.Add(basket.Id, basketEntry = basket);
                        basketEntry.BasketLines = new List<BasketLine>();
                    }
                    basketEntry.BasketLines.Add(basketLine);
                    return basketEntry;
                },
                new { UserId = userId },
                splitOn: $"{nameof(BasketLine.Id)}"
            );

            return basketDictionary.Values.FirstOrDefault();
        }
        catch (Exception ex)
        {
            throw new BasketNotFoundException($"Basket for User with ID {userId} not found.", ex);
        }
    }

    public async Task<Basket> GetSingleBasketWithDetailsByUserIdAsync(Guid userId)
    {
        try
        {
            var basketDictionary = new Dictionary<Guid, Basket>();
            var basketLinesDictionary = new Dictionary<Guid, BasketLine>();

            var basket = await _dbConnection.QueryAsync<Basket, User, BasketLine, Product, Size, Basket>(
                DapperConstants.GetSingleBasketDetailsQuery,
                (basket, user, basketLine, product, size) =>
                {
                    Basket basketEntry;
                    if (!basketDictionary.TryGetValue(basket.Id, out basketEntry))
                    {
                        basketDictionary.Add(basket.Id, basketEntry = basket);
                        basketEntry.User = user;
                        basketEntry.BasketLines = new List<BasketLine>();
                    }

                    BasketLine basketLineEntry;
                    if (!basketLinesDictionary.TryGetValue(basketLine.Id, out basketLineEntry))
                    {
                        basketLinesDictionary.Add(basketLine.Id, basketLineEntry = basketLine);
                        basketLineEntry.Product = product;
                        basketLineEntry.Size = size;
                        basketEntry.BasketLines.Add(basketLineEntry);
                    }

                    return basketEntry;
                },
                new { UserId = userId },
                splitOn: $"{nameof(User.Id)},{nameof(BasketLine.Id)},{nameof(Product.Id)},{nameof(Size.Id)}"
            );

            return basketDictionary.Values.FirstOrDefault();
        }
        catch (Exception ex)
        {
            throw new BasketNotFoundException($"Basket for User with ID {userId} not found.", ex);
        }
    }
}