using Microsoft.EntityFrameworkCore.Migrations;

namespace Template.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vozila_Kompanije_KompanijaId",
                table: "Vozila");

            migrationBuilder.AlterColumn<int>(
                name: "KompanijaId",
                table: "Vozila",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Vozila_Kompanije_KompanijaId",
                table: "Vozila",
                column: "KompanijaId",
                principalTable: "Kompanije",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vozila_Kompanije_KompanijaId",
                table: "Vozila");

            migrationBuilder.AlterColumn<int>(
                name: "KompanijaId",
                table: "Vozila",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vozila_Kompanije_KompanijaId",
                table: "Vozila",
                column: "KompanijaId",
                principalTable: "Kompanije",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
