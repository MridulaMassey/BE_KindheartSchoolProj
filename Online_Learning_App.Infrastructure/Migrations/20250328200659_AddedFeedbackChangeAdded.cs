using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedFeedbackChangeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "Activities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentPdfUrl",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_StudentId",
                table: "Activities",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Students_StudentId",
                table: "Activities",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Students_StudentId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_StudentId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "StudentPdfUrl",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Activities");
        }
    }
}
