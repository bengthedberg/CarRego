using Microsoft.EntityFrameworkCore;

namespace CarRego.Domain.VehicleManagement.Data;

public static class VehicleManagementContextExtensions
{
    public static Task<Vehicle?> VehicleWithVIN(this VehicleManagementContext context, VIN vin, bool asNoTracking = false)
    {
        IQueryable<Vehicle> query = context.Set<Vehicle>();

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        return query.SingleOrDefaultAsync(x => x.VIN == vin);
    }
    public static Task<Vehicle?> VehicleWithId(this VehicleManagementContext context, int id, bool asNoTracking = false)
    {
        IQueryable<Vehicle> query = context.Set<Vehicle>();

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        return query.SingleOrDefaultAsync(x => EF.Property<int>(x, "Id") == id);
    }

    public static Task<Person?> PersonWithId(this VehicleManagementContext context, int id, bool asNoTracking = false)
    {
        IQueryable<Person> query = context.Set<Person>();

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        return query.SingleOrDefaultAsync(x => EF.Property<int>(x, "id") == id);
    }
}