using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CarRego.Domain.Migrations;

public class DesignTimeMigrationContextFactory : IDesignTimeDbContextFactory<MigrationContext>
{
    public MigrationContext CreateDbContext(string[] args)
    {     
        var builder = new DbContextOptionsBuilder<MigrationContext>();
        builder.UseSqlServer(null);
        return new MigrationContext(builder.Options);
    }
}