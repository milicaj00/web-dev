using Microsoft.EntityFrameworkCore.Migrations;

namespace Template.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kategorija",
                table: "Filmovi");

            migrationBuilder.AddColumn<int>(
                name: "KategorijaId",
                table: "Filmovi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idProdKuce",
                table: "Filmovi",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KategorijaId",
                table: "Filmovi");

            migrationBuilder.DropColumn(
                name: "idProdKuce",
                table: "Filmovi");

            migrationBuilder.AddColumn<string>(
                name: "Kategorija",
                table: "Filmovi",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
