using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkedInClone.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddConversationsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_ReceivedUserId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "ReceivedUserId",
                table: "Messages",
                newName: "ReceivedById");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Messages",
                newName: "SentDate");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ReceivedUserId",
                table: "Messages",
                newName: "IX_Messages_ReceivedById");

            migrationBuilder.AddColumn<int>(
                name: "ConversationId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Conversation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceivedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversation_AspNetUsers_ReceivedById",
                        column: x => x.ReceivedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Conversation_AspNetUsers_StartedById",
                        column: x => x.StartedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationId",
                table: "Messages",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_ReceivedById",
                table: "Conversation",
                column: "ReceivedById");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_StartedById",
                table: "Conversation",
                column: "StartedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_ReceivedById",
                table: "Messages",
                column: "ReceivedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversation_ConversationId",
                table: "Messages",
                column: "ConversationId",
                principalTable: "Conversation",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_ReceivedById",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversation_ConversationId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "Conversation");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ConversationId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ConversationId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "SentDate",
                table: "Messages",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "ReceivedById",
                table: "Messages",
                newName: "ReceivedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ReceivedById",
                table: "Messages",
                newName: "IX_Messages_ReceivedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_ReceivedUserId",
                table: "Messages",
                column: "ReceivedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
