using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRego.Domain.PeopleManagement.Data.Config;

public class PersonTypeConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("People");

        builder.Property<int>("id");
        builder.HasMany<Address>("addresses").WithOne().HasForeignKey("PersonId").IsRequired();
        builder.Navigation("addresses").AutoInclude();

        builder.Ignore(x => x.DeliveryAddress);
        builder.Ignore(x => x.InvoiceAddress);

        builder.HasKey("id");
    }
}