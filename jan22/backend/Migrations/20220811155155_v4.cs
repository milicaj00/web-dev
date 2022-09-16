using Microsoft.EntityFrameworkCore.Migrations;

namespace Template.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "idProdKuce",
                table: "Filmovi");

            migrationBuilder.AddColumn<int>(
                name: "ProdukcijskaKucaId",
                table: "Kategorije",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kategorije_ProdukcijskaKucaId",
                table: "Kategorije",
                column: "ProdukcijskaKucaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kategorije_ProdukcijskaKuca_ProdukcijskaKucaId",
                table: "Kategorije",
                column: "ProdukcijskaKucaId",
                principalTable: "ProdukcijskaKuca",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kategorije_ProdukcijskaKuca_ProdukcijskaKucaId",
                table: "Kategorije");

            migrationBuilder.DropIndex(
                name: "IX_Kategorije_ProdukcijskaKucaId",
                table: "Kategorije");

            migrationBuilder.DropColumn(
                name: "ProdukcijskaKucaId",
                table: "Kategorije");

            migrationBuilder.AddColumn<int>(
                name: "idProdKuce",
                table: "Filmovi",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
