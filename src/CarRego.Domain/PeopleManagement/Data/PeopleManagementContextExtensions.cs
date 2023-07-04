using Microsoft.EntityFrameworkCore;

namespace CarRego.Domain.PeopleManagement.Data;

public static class PeopleManagementContextExtensions
{
    public static Task<Person?> PersonWithId(this PeopleManagementContext context, int id, bool asNoTracking = false)
    {
        IQueryable<Person> query = context.Set<Person>();

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        return query.SingleOrDefaultAsync(x => EF.Property<int>(x, "id") == id);
    }
}