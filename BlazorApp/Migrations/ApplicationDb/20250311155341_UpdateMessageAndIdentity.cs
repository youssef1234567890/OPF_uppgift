using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class UpdateMessageAndIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Messages",
                newName: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ApplicationUserId",
                table: "Messages",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_ApplicationUserId",
                table: "Messages",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_ApplicationUserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ApplicationUserId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Messages",
                newName: "UserName");
        }
    }
}
