namespace Warehouse.Persistence.PostgreSQL.Configuration.Contstants;

public class DapperConstants
{
    public const string GetProductsDetailsQuery = """
    SELECT p.*, b.*, pg.*, ps.*, s.*, g.*
    FROM "Products" p
    LEFT JOIN "Brands" b ON p."BrandId" = b."Id"
    LEFT JOIN "ProductGroups" pg ON p."Id" = pg."ProductId"
    LEFT JOIN "Groups" g ON pg."GroupId" = g."Id"
    LEFT JOIN "ProductSizes" ps ON p."Id" = ps."ProductId"
    LEFT JOIN "Sizes" s ON ps."SizeId" = s."Id"
    WHERE p."IsDeleted" = false
    GROUP BY p."Id", b."Id", pg."ProductId", pg."GroupId", g."Id", ps."ProductId", ps."SizeId", s."Id"
    """;

    public const string GetProductsDetailsSingleQuery = """
    SELECT p.*, b.*, pg.*, ps.*, s.*, g.*
    FROM "Products" p
    LEFT JOIN "Brands" b ON p."BrandId" = b."Id"
    LEFT JOIN "ProductGroups" pg ON p."Id" = pg."ProductId"
    LEFT JOIN "Groups" g ON pg."GroupId" = g."Id"
    LEFT JOIN "ProductSizes" ps ON p."Id" = ps."ProductId"
    LEFT JOIN "Sizes" s ON ps."SizeId" = s."Id"
    WHERE p."IsDeleted" = false
    AND p."Id" = @ProductId
    GROUP BY p."Id", b."Id", pg."ProductId", pg."GroupId", g."Id", ps."ProductId", ps."SizeId", s."Id"
    """;

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