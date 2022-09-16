using Microsoft.EntityFrameworkCore.Migrations;

namespace Template.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brendovi_Prodavnice_ProdavnicaId",
                table: "Brendovi");

            migrationBuilder.DropForeignKey(
                name: "FK_Tipovi_Prodavnice_ProdavnicaId",
                table: "Tipovi");

            migrationBuilder.DropIndex(
                name: "IX_Tipovi_ProdavnicaId",
                table: "Tipovi");

            migrationBuilder.DropIndex(
                name: "IX_Brendovi_ProdavnicaId",
                table: "Brendovi");

            migrationBuilder.DropColumn(
                name: "ProdavnicaId",
                table: "Tipovi");

            migrationBuilder.DropColumn(
                name: "ProdavnicaId",
                table: "Brendovi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProdavnicaId",
                table: "Tipovi",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProdavnicaId",
                table: "Brendovi",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tipovi_ProdavnicaId",
                table: "Tipovi",
                column: "ProdavnicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Brendovi_ProdavnicaId",
                table: "Brendovi",
                column: "ProdavnicaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brendovi_Prodavnice_ProdavnicaId",
                table: "Brendovi",
                column: "ProdavnicaId",
                principalTable: "Prodavnice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tipovi_Prodavnice_ProdavnicaId",
                table: "Tipovi",
                column: "ProdavnicaId",
                principalTable: "Prodavnice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
