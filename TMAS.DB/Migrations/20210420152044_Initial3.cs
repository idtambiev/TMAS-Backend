using Microsoft.EntityFrameworkCore.Migrations;

namespace TMAS.BLL.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Columns_Boards_BoardId",
                table: "Columns");

            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Boards_BoardId",
                table: "Histories");

            migrationBuilder.AddForeignKey(
                name: "FK_Columns_Boards_BoardId",
                table: "Columns",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Boards_BoardId",
                table: "Histories",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                 onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Columns_Boards_BoardId",
                table: "Columns");

            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Boards_BoardId",
                table: "Histories");

            migrationBuilder.AddForeignKey(
                name: "FK_Columns_Boards_BoardId",
                table: "Columns",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
