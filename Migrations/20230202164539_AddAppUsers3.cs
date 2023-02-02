using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkedInClone.Migrations
{
    /// <inheritdoc />
    public partial class AddAppUsers3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            
            // migrationBuilder.DropForeignKey(
            //     name: "FK_AspNetUserClaims_AspNetUsers_UserId",
            //     table: "AspNetUserClaims");

            // migrationBuilder.DropForeignKey(
            //     name: "FK_AspNetUserLogins_AspNetUsers_UserId",
            //     table: "AspNetUserLogins");

            // migrationBuilder.DropForeignKey(
            //     name: "FK_AspNetUserRoles_AspNetUsers_UserId",
            //     table: "AspNetUserRoles");

            // migrationBuilder.DropForeignKey(
            //     name: "FK_AspNetUserTokens_AspNetUsers_UserId",
            //     table: "AspNetUserTokens");

            // migrationBuilder.DropForeignKey(
            //     name: "FK_Comments_AspNetUsers_AuthorId",
            //     table: "Comments");

            // migrationBuilder.DropForeignKey(
            //     name: "FK_Connections_AspNetUsers_ReceiverId",
            //     table: "Connections");

            // migrationBuilder.DropForeignKey(
            //     name: "FK_Connections_AspNetUsers_SenderId",
            //     table: "Connections");

            // migrationBuilder.DropForeignKey(
            //     name: "FK_JobApplications_AspNetUsers_ApplicantId",
            //     table: "JobApplications");

            // migrationBuilder.DropForeignKey(
            //     name: "FK_JobPosting_AspNetUsers_RecruiterId",
            //     table: "JobPosting");

            // migrationBuilder.DropForeignKey(
            //     name: "FK_Likes_AspNetUsers_LikedById",
            //     table: "Likes");

            // migrationBuilder.DropForeignKey(
            //     name: "FK_Messages_AspNetUsers_ReceivedUserId",
            //     table: "Messages");

            // migrationBuilder.DropForeignKey(
            //     name: "FK_Messages_AspNetUsers_SentById",
            //     table: "Messages");

            // migrationBuilder.DropForeignKey(
            //     name: "FK_Posts_AspNetUsers_AuthorId",
            //     table: "Posts");

            // migrationBuilder.DropPrimaryKey(
            //     name: "PK_AspNetUsers",
            //     table: "AspNetUsers");

            // migrationBuilder.RenameTable(
            //     name: "AspNetUsers",
            //     newName: "AppUsers");

            // migrationBuilder.AddPrimaryKey(
            //     name: "PK_AppUsers",
            //     table: "AppUsers",
            //     column: "Id");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_AspNetUserClaims_AppUsers_UserId",
            //     table: "AspNetUserClaims",
            //     column: "UserId",
            //     principalTable: "AppUsers",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Restrict);

            // migrationBuilder.AddForeignKey(
            //     name: "FK_AspNetUserLogins_AppUsers_UserId",
            //     table: "AspNetUserLogins",
            //     column: "UserId",
            //     principalTable: "AppUsers",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Restrict);

            // migrationBuilder.AddForeignKey(
            //     name: "FK_AspNetUserRoles_AppUsers_UserId",
            //     table: "AspNetUserRoles",
            //     column: "UserId",
            //     principalTable: "AppUsers",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Restrict);

            // migrationBuilder.AddForeignKey(
            //     name: "FK_AspNetUserTokens_AppUsers_UserId",
            //     table: "AspNetUserTokens",
            //     column: "UserId",
            //     principalTable: "AppUsers",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Restrict);

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Comments_AppUsers_AuthorId",
            //     table: "Comments",
            //     column: "AuthorId",
            //     principalTable: "AppUsers",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Restrict);

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Connections_AppUsers_ReceiverId",
            //     table: "Connections",
            //     column: "ReceiverId",
            //     principalTable: "AppUsers",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Restrict);

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Connections_AppUsers_SenderId",
            //     table: "Connections",
            //     column: "SenderId",
            //     principalTable: "AppUsers",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Restrict);

            // migrationBuilder.AddForeignKey(
            //     name: "FK_JobApplications_AppUsers_ApplicantId",
            //     table: "JobApplications",
            //     column: "ApplicantId",
            //     principalTable: "AppUsers",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Restrict);

            // migrationBuilder.AddForeignKey(
            //     name: "FK_JobPosting_AppUsers_RecruiterId",
            //     table: "JobPosting",
            //     column: "RecruiterId",
            //     principalTable: "AppUsers",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Restrict);

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Likes_AppUsers_LikedById",
            //     table: "Likes",
            //     column: "LikedById",
            //     principalTable: "AppUsers",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Restrict);

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Messages_AppUsers_ReceivedUserId",
            //     table: "Messages",
            //     column: "ReceivedUserId",
            //     principalTable: "AppUsers",
            //     principalColumn: "Id");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Messages_AppUsers_SentById",
            //     table: "Messages",
            //     column: "SentById",
            //     principalTable: "AppUsers",
            //     principalColumn: "Id");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Posts_AppUsers_AuthorId",
            //     table: "Posts",
            //     column: "AuthorId",
            //     principalTable: "AppUsers",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AppUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AppUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AppUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AppUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AppUsers_AuthorId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Connections_AppUsers_ReceiverId",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_Connections_AppUsers_SenderId",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_AppUsers_ApplicantId",
                table: "JobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPosting_AppUsers_RecruiterId",
                table: "JobPosting");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AppUsers_LikedById",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AppUsers_ReceivedUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AppUsers_SentById",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AppUsers_AuthorId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUsers",
                table: "AppUsers");

            migrationBuilder.RenameTable(
                name: "AppUsers",
                newName: "AspNetUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_AuthorId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_AspNetUsers_ReceiverId",
                table: "Connections",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_AspNetUsers_SenderId",
                table: "Connections",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_AspNetUsers_ApplicantId",
                table: "JobApplications",
                column: "ApplicantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosting_AspNetUsers_RecruiterId",
                table: "JobPosting",
                column: "RecruiterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_LikedById",
                table: "Likes",
                column: "LikedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_ReceivedUserId",
                table: "Messages",
                column: "ReceivedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SentById",
                table: "Messages",
                column: "SentById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_AuthorId",
                table: "Posts",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
