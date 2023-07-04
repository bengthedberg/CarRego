using Microsoft.EntityFrameworkCore;

namespace CarRego.Domain.Migrations;

public class MigrationContext : DbContext
{
    public MigrationContext() { }
    public MigrationContext(DbContextOptions<MigrationContext> options)
        : base(options) { }
}