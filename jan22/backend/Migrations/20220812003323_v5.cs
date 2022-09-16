using Microsoft.EntityFrameworkCore.Migrations;

namespace Template.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ocena",
                table: "Filmovi",
                newName: "ProsecnaOcena");

            migrationBuilder.AddColumn<int>(
                name: "BrojOcena",
                table: "Filmovi",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrojOcena",
                table: "Filmovi");

            migrationBuilder.RenameColumn(
                name: "ProsecnaOcena",
                table: "Filmovi",
                newName: "Ocena");
        }
    }
}
