using Dapper;
using System.Data;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities.Orders;
using Warehouse.Domain.Entities.Products;
using Warehouse.Domain.Entities.Users;
using Warehouse.Persistence.Abstractions;
using Warehouse.Persistence.PostgreSQL.Configuration.Contstants;
using Warehouse.Persistence.PostgreSQL.Persistence.Contexts;

namespace Warehouse.Persistence.PostgreSQL.Persistence.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(WarehouseDbContext dbContext, IDbConnection dbConnection) : base(dbContext, dbConnection)
    {
    }

    public async Task<Order> GetSingleOrderAsync(Guid orderId)
    {
        var orderDictionary = new Dictionary<Guid, Order>();
        var order = await _dbConnection.QueryAsync<Order, User, OrderStatus, Order>(
            DapperConstants.GetSingleOrdersQuery,
            (o, u, os) =>
            {
                Order orderEntry;
                if (!orderDictionary.TryGetValue(o.Id, out orderEntry))
                {
                    orderDictionary.Add(o.Id, orderEntry = o);
                    orderEntry.User = u;
                    orderEntry.Status = os;
                    orderEntry.OrderLines = orderEntry.OrderLines ?? new List<OrderLine>();
                }
                return orderEntry;
            },
            new { OrderId = orderId },
            splitOn: $"{nameof(User.Id)},{nameof(OrderStatus.Id)}"
        );

        var orderLines = await _dbConnection.QueryAsync<OrderLine, Product, ProductSize, Size, OrderLine>(
            DapperConstants.GetOrderLinesQuery,
            (od, p, ps, s) =>
            {
                od.Product = p;
                od.SizeId = s.Id;
                ps.Size = s;
                od.Size = s;
                return od;
            },
            new { OrderId = orderId },
            splitOn: $"{nameof(Product.Id)},{nameof(ProductSize.ProductId)},{nameof(Size.Id)}"
        );

        foreach (var orderLine in orderLines)
        {
            orderDictionary[orderId].OrderLines.Add(orderLine);
        }

        return orderDictionary.ContainsKey(orderId) ? orderDictionary[orderId] : null;
    }

    public async Task<IEnumerable<Order>> GetOrdersListAsync()
    {
        var lookup = new Dictionary<Guid, Order>();
        var result = await _dbConnection.QueryAsync<Order, User, OrderStatus, Order>(
            DapperConstants.GetAllOrdersQuery,
            (order, user, orderstatus) =>
            {
                Order orderEntry;
                if (!lookup.TryGetValue(order.Id, out orderEntry))
                {
                    lookup.Add(order.Id, orderEntry = order);
                    orderEntry.User = user;
                    orderEntry.Status = orderstatus;
                }
                return orderEntry;
            },
            splitOn: $"{nameof(User.Id)},{nameof(OrderStatus.Id)}"
        );

        return result;
    }
}