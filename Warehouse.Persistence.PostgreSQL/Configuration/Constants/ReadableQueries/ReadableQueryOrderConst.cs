namespace Warehouse.Persistence.PostgreSQL.Configuration.Constants.ReadableQueries;
public static class ReadableQueryOrderConst
{
    public const string GetAllOrdersQuery = """
    SELECT o.*, u.*, s.*
    FROM "Orders" o
    INNER JOIN "Users" u ON o."UserId" = u."Id"
    INNER JOIN "OrderStatus" s ON o."StatusId" = s."Id"
    WHERE o."IsDeleted" = false
    """;

    public const string GetSingleOrdersQuery = """
    SELECT o.*, u.*, s.*
    FROM "Orders" o
    INNER JOIN "Users" u ON o."UserId" = u."Id"
    INNER JOIN "OrderStatus" s ON o."StatusId" = s."Id"
    WHERE o."Id" = @OrderId
    AND o."IsDeleted" = false
    """;

    public const string GetOrderLinesQuery = """
    SELECT od.*, p.*, ps.*, s.*
    FROM  "OrderLines" od
    INNER JOIN "Products" p ON od."ProductId" = p."Id"
    INNER JOIN "ProductSizes" ps ON od."ProductId" = ps."ProductId"
    INNER JOIN "Sizes" s ON ps."SizeId" = s."Id"
    WHERE od."OrderId" = @OrderId
    """;
}
