using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkedInClone.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddConversationsTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversation_AspNetUsers_ReceivedById",
                table: "Conversation");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversation_AspNetUsers_StartedById",
                table: "Conversation");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversation_ConversationId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conversation",
                table: "Conversation");

            migrationBuilder.RenameTable(
                name: "Conversation",
                newName: "Conversations");

            migrationBuilder.RenameIndex(
                name: "IX_Conversation_StartedById",
                table: "Conversations",
                newName: "IX_Conversations_StartedById");

            migrationBuilder.RenameIndex(
                name: "IX_Conversation_ReceivedById",
                table: "Conversations",
                newName: "IX_Conversations_ReceivedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conversations",
                table: "Conversations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_ReceivedById",
                table: "Conversations",
                column: "ReceivedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_StartedById",
                table: "Conversations",
                column: "StartedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_ReceivedById",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_StartedById",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conversations",
                table: "Conversations");

            migrationBuilder.RenameTable(
                name: "Conversations",
                newName: "Conversation");

            migrationBuilder.RenameIndex(
                name: "IX_Conversations_StartedById",
                table: "Conversation",
                newName: "IX_Conversation_StartedById");

            migrationBuilder.RenameIndex(
                name: "IX_Conversations_ReceivedById",
                table: "Conversation",
                newName: "IX_Conversation_ReceivedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conversation",
                table: "Conversation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversation_AspNetUsers_ReceivedById",
                table: "Conversation",
                column: "ReceivedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversation_AspNetUsers_StartedById",
                table: "Conversation",
                column: "StartedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversation_ConversationId",
                table: "Messages",
                column: "ConversationId",
                principalTable: "Conversation",
                principalColumn: "Id");
        }
    }
}
