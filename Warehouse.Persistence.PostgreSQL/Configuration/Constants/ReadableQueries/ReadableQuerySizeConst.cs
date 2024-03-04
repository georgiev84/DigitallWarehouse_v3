namespace Warehouse.Persistence.PostgreSQL.Configuration.Constants.ReadableQueries;
public static class ReadableQuerySizeConst
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
