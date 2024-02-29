using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Persistence.Abstractions.Constants;
public class DapperConstants
{
    public const string GetById = """SELECT * FROM "{0}" WHERE "Id" = @Id""";
}
