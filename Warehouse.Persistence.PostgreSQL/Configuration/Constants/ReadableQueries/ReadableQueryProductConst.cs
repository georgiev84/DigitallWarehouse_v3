namespace Warehouse.Persistence.PostgreSQL.Configuration.Constants.ReadableQueries;
public static class ReadableQueryProductConst
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
}
