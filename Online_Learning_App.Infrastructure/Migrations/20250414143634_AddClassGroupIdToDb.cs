using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClassGroupIdToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClassgroupId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClassgroupId",
                table: "Users",
                column: "ClassgroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ClassGroups_ClassgroupId",
                table: "Users",
                column: "ClassgroupId",
                principalTable: "ClassGroups",
                principalColumn: "ClassGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ClassGroups_ClassgroupId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ClassgroupId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ClassgroupId",
                table: "Users");
        }
    }
}
