using Microsoft.EntityFrameworkCore.Migrations;

namespace TMAS.BLL.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_AspNetUsers_BoardUserId",
                table: "Boards");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_AspNetUsers_BoardUserId",
                table: "Boards",
                column: "BoardUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_AspNetUsers_BoardUserId",
                table: "Boards");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_AspNetUsers_BoardUserId",
                table: "Boards",
                column: "BoardUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
