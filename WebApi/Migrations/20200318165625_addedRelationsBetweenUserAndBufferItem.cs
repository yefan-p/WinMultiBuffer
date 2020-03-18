using Microsoft.EntityFrameworkCore.Migrations;

namespace MultiBuffer.WebApi.Migrations
{
    public partial class addedRelationsBetweenUserAndBufferItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "BufferItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BufferItems_UserId",
                table: "BufferItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BufferItems_Users_UserId",
                table: "BufferItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BufferItems_Users_UserId",
                table: "BufferItems");

            migrationBuilder.DropIndex(
                name: "IX_BufferItems_UserId",
                table: "BufferItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BufferItems");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
