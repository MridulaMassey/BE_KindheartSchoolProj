using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_App.Infrastructure.migrations
{
    /// <inheritdoc />
    public partial class SubjecteKeyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClassGroupSubjectId",
                table: "Activities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ClassGroupSubjectId",
                table: "Activities",
                column: "ClassGroupSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_ClassGroupSubject_ClassGroupSubjectId",
                table: "Activities",
                column: "ClassGroupSubjectId",
                principalTable: "ClassGroupSubject",
                principalColumn: "ClassGroupSubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_ClassGroupSubject_ClassGroupSubjectId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_ClassGroupSubjectId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ClassGroupSubjectId",
                table: "Activities");
        }
    }
}
