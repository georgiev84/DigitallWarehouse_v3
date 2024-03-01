namespace Warehouse.Persistence.PostgreSQL.Configuration.Contstants.DapperBasketConstants;
public static class DapperBasketReadConst
{
    public const string GetSingleBasketQuery = """
    SELECT b.*, bl.*
    FROM "Baskets" b
    LEFT JOIN "BasketLines" bl ON b."Id" = bl."BasketId"
    WHERE b."UserId" = @UserId
    """;

    public const string GetSingleBasketDetailsQuery = """
    SELECT b.*, u.*, bl.*, p.*, s.*
    FROM "Baskets" b
    LEFT JOIN "Users" u ON b."UserId" = u."Id"
    LEFT JOIN "BasketLines" bl ON b."Id" = bl."BasketId"
    LEFT JOIN "Products" p ON bl."ProductId" = p."Id"
    LEFT JOIN "Sizes" s ON bl."SizeId" = s."Id"
    WHERE b."UserId" = @UserId
    """;
}
