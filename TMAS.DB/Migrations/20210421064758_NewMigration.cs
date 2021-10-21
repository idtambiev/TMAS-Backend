using Microsoft.EntityFrameworkCore.Migrations;

namespace TMAS.BLL.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardsAccesses_AspNetUsers_UserId",
                table: "BoardsAccesses");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardsAccesses_Boards_BoardId",
                table: "BoardsAccesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Columns_Boards_BoardId",
                table: "Columns");

            migrationBuilder.DropForeignKey(
                name: "FK_Histories_AspNetUsers_AuthorId",
                table: "Histories");

            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Boards_BoardId",
                table: "Histories");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardsAccesses_AspNetUsers_UserId",
                table: "BoardsAccesses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardsAccesses_Boards_BoardId",
                table: "BoardsAccesses",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Columns_Boards_BoardId",
                table: "Columns",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_AspNetUsers_AuthorId",
                table: "Histories",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Boards_BoardId",
                table: "Histories",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardsAccesses_AspNetUsers_UserId",
                table: "BoardsAccesses");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardsAccesses_Boards_BoardId",
                table: "BoardsAccesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Columns_Boards_BoardId",
                table: "Columns");

            migrationBuilder.DropForeignKey(
                name: "FK_Histories_AspNetUsers_AuthorId",
                table: "Histories");

            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Boards_BoardId",
                table: "Histories");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardsAccesses_AspNetUsers_UserId",
                table: "BoardsAccesses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardsAccesses_Boards_BoardId",
                table: "BoardsAccesses",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Columns_Boards_BoardId",
                table: "Columns",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_AspNetUsers_AuthorId",
                table: "Histories",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Boards_BoardId",
                table: "Histories",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id");
        }
    }
}
