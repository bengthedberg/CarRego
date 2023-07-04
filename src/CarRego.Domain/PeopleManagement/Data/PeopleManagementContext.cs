using Microsoft.EntityFrameworkCore;

namespace CarRego.Domain.PeopleManagement.Data;

public class PeopleManagementContext : DbContext
{
    public PeopleManagementContext() { }
    public PeopleManagementContext(DbContextOptions<PeopleManagementContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Unordered...only works if order doesn't matter
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}