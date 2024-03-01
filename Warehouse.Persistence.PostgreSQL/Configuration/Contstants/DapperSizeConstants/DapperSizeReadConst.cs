namespace Warehouse.Persistence.PostgreSQL.Configuration.Contstants.DapperSizeConstants;
public static class DapperSizeReadConst
{
    public const string GetProductSizeQuery = """
    SELECT *
    FROM "ProductSizes"
    WHERE "ProductId" = @ProductId AND "SizeId" = @SizeId
    """;

    public const string GetSizeNames = """
    SELECT "Name"
    FROM "Sizes"
    """;
}
