using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CarRego.Domain.UnitTests.Infrastructure;

public class TestBase<TCtx, TSvc> where TCtx : DbContext
{
    protected static async Task RunTest(Func<TCtx, TSvc> service, Func<DbCommand, Task> prepare, Func<TSvc, Task> execute, Func<DbCommand, Task>? validate = null)
    {
        var context = TestHelper.GetContext<TCtx>() ?? throw new Exception("Invalid context type");

        IDbContextTransaction? transaction = null;
        try
        {
            transaction = context.Database.BeginTransaction();

            var conn = context.Database.GetDbConnection();
            using (var cmd = conn.CreateCommand())
            {
                cmd.Transaction = transaction.GetDbTransaction();
                
                await prepare(cmd);

                await execute(service(context));

                if (validate != null)
                    await validate(cmd);
            }
        }
        finally
        {
            transaction?.Rollback();
        }
    }

}
