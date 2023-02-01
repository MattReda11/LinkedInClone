using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkedInClone.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePostsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.AddColumn<string>(
            //     name: "AuthorId",
            //     table: "Posts",
            //     type: "nvarchar(450)",
            //     nullable: false,
            //     defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            // migrationBuilder.AddColumn<DateTime>(
            //     name: "PostedDate",
            //     table: "Posts",
            //     type: "datetime2",
            //     nullable: false,
            //     defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            // migrationBuilder.CreateIndex(
            //     name: "IX_Posts_AuthorId",
            //     table: "Posts",
            //     column: "AuthorId");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Posts_AspNetUsers_AuthorId",
            //     table: "Posts",
            //     column: "AuthorId",
            //     principalTable: "AspNetUsers",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropForeignKey(
            //     name: "FK_Posts_AspNetUsers_AuthorId",
            //     table: "Posts");

            // migrationBuilder.DropIndex(
            //     name: "IX_Posts_AuthorId",
            //     table: "Posts");

            // migrationBuilder.DropColumn(
            //     name: "AuthorId",
            //     table: "Posts");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Posts");

            // migrationBuilder.DropColumn(
            //     name: "PostedDate",
            //     table: "Posts");
        }
    }
}
