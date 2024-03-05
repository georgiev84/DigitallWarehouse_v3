namespace Warehouse.Persistence.PostgreSQL.Configuration.Constants.MutatableQueries;
public static class MutatableQueryProductConst
{
    public const string UpdateProductQuery = """
    UPDATE "Products"
    SET
    "Title" = @Title,
    "Description" = @Description,
    "Price" = @Price,
    "IsDeleted" = @IsDeleted
    WHERE "Id" = @Id
    """;

    public const string DeleteProductSizesQuery = """
    DELETE FROM "ProductSizes"
    WHERE "ProductId" = @ProductId
    """;

    public const string InsertProductSizesQuery = """
    INSERT INTO "ProductSizes" ("ProductId", "SizeId", "QuantityInStock")
    VALUES (@ProductId, @SizeId, @QuantityInStock)
    """;

    public const string DeleteProductGroupsQuery = """
    DELETE FROM "ProductGroups"
    WHERE "ProductId" = @ProductId
    """;

    public const string InsertProductGroupsQuery = """
    INSERT INTO "ProductGroups" ("ProductId", "GroupId")
    VALUES (@ProductId, @GroupId)
    """;

    public const string InsertProductQuery = """
    INSERT INTO "Products" ("Id", "BrandId", "Title", "Description", "Price", "IsDeleted")
    VALUES (@Id, @BrandId, @Title, @Description, @Price, @IsDeleted)
    """;

    public const string SetDeleteProductQuery = """
    UPDATE "Products" SET "IsDeleted" = @IsDeleted
    WHERE "Id" = @Id
    """;
}
