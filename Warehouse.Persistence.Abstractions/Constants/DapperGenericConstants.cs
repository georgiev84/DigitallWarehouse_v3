namespace Warehouse.Persistence.Abstractions.Constants;

public class DapperGenericConstants
{
    public const string GetById = """SELECT * FROM "{0}" WHERE "Id" = @Id""";
}