using Microsoft.EntityFrameworkCore.Migrations;

namespace TMAS.BLL.Migrations
{
    public partial class NewMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Boards_BoardId",
                table: "Histories");

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Boards_BoardId",
                table: "Histories",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Boards_BoardId",
                table: "Histories");

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Boards_BoardId",
                table: "Histories",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
