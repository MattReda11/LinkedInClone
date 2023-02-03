using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkedInClone.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingJobPostingFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobPosting_JobPostingId",
                table: "JobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPosting_AspNetUsers_RecruiterId",
                table: "JobPosting");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_JobPostingId",
                table: "JobApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobPosting",
                table: "JobPosting");

            migrationBuilder.DropColumn(
                name: "JobPostingId",
                table: "JobApplications");

            migrationBuilder.RenameTable(
                name: "JobPosting",
                newName: "JobPostings");

            migrationBuilder.RenameIndex(
                name: "IX_JobPosting_RecruiterId",
                table: "JobPostings",
                newName: "IX_JobPostings_RecruiterId");

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "JobApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobPostings",
                table: "JobPostings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobId",
                table: "JobApplications",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobPostings_JobId",
                table: "JobApplications",
                column: "JobId",
                principalTable: "JobPostings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobPostings_AspNetUsers_RecruiterId",
                table: "JobPostings",
                column: "RecruiterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobPostings_JobId",
                table: "JobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPostings_AspNetUsers_RecruiterId",
                table: "JobPostings");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_JobId",
                table: "JobApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobPostings",
                table: "JobPostings");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "JobApplications");

            migrationBuilder.RenameTable(
                name: "JobPostings",
                newName: "JobPosting");

            migrationBuilder.RenameIndex(
                name: "IX_JobPostings_RecruiterId",
                table: "JobPosting",
                newName: "IX_JobPosting_RecruiterId");

            migrationBuilder.AddColumn<int>(
                name: "JobPostingId",
                table: "JobApplications",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobPosting",
                table: "JobPosting",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobPostingId",
                table: "JobApplications",
                column: "JobPostingId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobPosting_JobPostingId",
                table: "JobApplications",
                column: "JobPostingId",
                principalTable: "JobPosting",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosting_AspNetUsers_RecruiterId",
                table: "JobPosting",
                column: "RecruiterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
