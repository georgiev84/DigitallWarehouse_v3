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