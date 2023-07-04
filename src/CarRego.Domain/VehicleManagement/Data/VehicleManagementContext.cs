using Microsoft.EntityFrameworkCore;

namespace CarRego.Domain.VehicleManagement.Data;
public class VehicleManagementContext : DbContext
{
    public VehicleManagementContext() { }
    public VehicleManagementContext(DbContextOptions<VehicleManagementContext> options)
        : base(options) { }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<VIN>()
            .HaveConversion<VINConverter>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehicle>(x =>
        {
            x.ToTable("Vehicles");

            x.Property<int>("Id");
            
            // x.Property(x => x.VIN).HasConversion(y => y.Value, y => VIN.Create(y));
            // x.Property(x => x.VIN).HasConversion<VINConverter>();
            
            x.HasMany<Owner>("owners").WithOne().HasForeignKey("VehicleId");
            x.Navigation("owners").AutoInclude();

            x.Ignore(x => x.CurrentOwner);
            x.Ignore(x => x.PreviousOwners);

            x.HasKey("Id");
        });

        modelBuilder.Entity<Owner>(x =>
        {
            x.ToTable("VehicleOwners");

            x.Property<int>("VehicleId");
            x.HasOne<Person>("person").WithMany().HasForeignKey("PersonId");
            x.Navigation("person").AutoInclude();

            x.Ignore(x => x.Id);
            x.Ignore(x => x.FirstName);
            x.Ignore(x => x.LastName);

            x.HasKey("VehicleId", "PersonId");
        });

        modelBuilder.Entity<Person>(x =>
        {
            x.ToTable("People");

            x.Property<int>("id");

            x.HasKey("id");
        });
    }
}