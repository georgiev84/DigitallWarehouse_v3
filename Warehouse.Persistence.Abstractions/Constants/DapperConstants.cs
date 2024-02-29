namespace Warehouse.Persistence.Abstractions.Constants;

public class DapperConstants
{
    public const string GetById = """SELECT * FROM "{0}" WHERE "Id" = @Id""";
}