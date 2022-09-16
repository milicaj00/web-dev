using Microsoft.EntityFrameworkCore.Migrations;

namespace Template.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Komponenta",
                table: "Spojevi");

            migrationBuilder.AddColumn<int>(
                name: "KomponentaId",
                table: "Spojevi",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Spojevi_KomponentaId",
                table: "Spojevi",
                column: "KomponentaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Spojevi_Komponente_KomponentaId",
                table: "Spojevi",
                column: "KomponentaId",
                principalTable: "Komponente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spojevi_Komponente_KomponentaId",
                table: "Spojevi");

            migrationBuilder.DropIndex(
                name: "IX_Spojevi_KomponentaId",
                table: "Spojevi");

            migrationBuilder.DropColumn(
                name: "KomponentaId",
                table: "Spojevi");

            migrationBuilder.AddColumn<int>(
                name: "Komponenta",
                table: "Spojevi",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
