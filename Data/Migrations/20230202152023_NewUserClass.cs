using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkedInClone.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewUserClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                    


            migrationBuilder.DropForeignKey(
                name: "FK_Connections_AspNetUsers_AccountOwnerId",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_Connections_AspNetUsers_FriendId",
                table: "Connections");

            // migrationBuilder.DropIndex(
            //     name: "IX_Connections_AccountOwnerId",
            //     table: "Connections");

            // migrationBuilder.DropIndex(
            //     name: "IX_Connections_FriendId",
            //     table: "Connections");

       

            migrationBuilder.DropColumn(
                name: "AccountOwnerId",
                table: "Connections");

            migrationBuilder.DropColumn(
                name: "FriendId",
                table: "Connections");
            

            migrationBuilder.RenameIndex(
                name: "IX_Comment_PostId",
                table: "Comments",
                newName: "IX_Comments_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_AuthorId",
                table: "Comments",
                newName: "IX_Comments_AuthorId");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverId",
                table: "Connections",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderId",
                table: "Connections",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            // migrationBuilder.AddPrimaryKey(
            //     name: "PK_Comments",
            //     table: "Comments",
            //     column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_ReceiverId",
                table: "Connections",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_SenderId",
                table: "Connections",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_AuthorId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_AuthorId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Connections_AspNetUsers_ReceiverId",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_Connections_AspNetUsers_SenderId",
                table: "Connections");

            migrationBuilder.DropIndex(
                name: "IX_Connections_ReceiverId",
                table: "Connections");

            migrationBuilder.DropIndex(
                name: "IX_Connections_SenderId",
                table: "Connections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "Connections");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Connections");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_PostId",
                table: "Comment",
                newName: "IX_Comment_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_AuthorId",
                table: "Comment",
                newName: "IX_Comment_AuthorId");

            migrationBuilder.AddColumn<string>(
                name: "AccountOwnerId",
                table: "Connections",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FriendId",
                table: "Connections",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_AccountOwnerId",
                table: "Connections",
                column: "AccountOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_FriendId",
                table: "Connections",
                column: "FriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_AuthorId",
                table: "Comment",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Posts_PostId",
                table: "Comment",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_AspNetUsers_AccountOwnerId",
                table: "Connections",
                column: "AccountOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_AspNetUsers_FriendId",
                table: "Connections",
                column: "FriendId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
