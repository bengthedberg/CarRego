using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRego.Domain.Migrations;

[Migration("20230703_0002_PersonServiceMigration")]
[DbContext(typeof(MigrationContext))]
public class PersonServiceMigration : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Addresses",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Type = table.Column<string>(type: "nvarchar(16)", nullable: false),
                AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                PersonId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Addresses", x => x.Id);
                table.ForeignKey(
                    name: "FK_Addresses_People_PersonId",
                    column: x => x.PersonId,
                    principalTable: "People",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Addresses_PersonId",
            table: "Addresses",
            column: "PersonId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Addresses");
    }
}