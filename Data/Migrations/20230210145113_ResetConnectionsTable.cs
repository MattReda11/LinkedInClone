using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkedInClone.Data.Migrations
{
    /// <inheritdoc />
    public partial class ResetConnectionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_AspNetUsers_ReceiverId",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_Connections_AspNetUsers_SenderId",
                table: "Connections");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Connections",
                newName: "AccountOwnerId");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Connections",
                newName: "FriendId");

            migrationBuilder.RenameIndex(
                name: "IX_Connections_SenderId",
                table: "Connections",
                newName: "IX_Connections_AccountOwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Connections_ReceiverId",
                table: "Connections",
                newName: "IX_Connections_FriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_AspNetUsers_AccountOwnerId",
                table: "Connections",
                column: "AccountOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_AspNetUsers_FriendId",
                table: "Connections",
                column: "FriendId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_AspNetUsers_AccountOwnerId",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_Connections_AspNetUsers_FriendId",
                table: "Connections");

            migrationBuilder.RenameColumn(
                name: "FriendId",
                table: "Connections",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "AccountOwnerId",
                table: "Connections",
                newName: "ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Connections_FriendId",
                table: "Connections",
                newName: "IX_Connections_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Connections_AccountOwnerId",
                table: "Connections",
                newName: "IX_Connections_ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_AspNetUsers_ReceiverId",
                table: "Connections",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_AspNetUsers_SenderId",
                table: "Connections",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
