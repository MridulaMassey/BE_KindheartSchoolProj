using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedSubmissionEntitychange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubmissionId",
                table: "ClassGroupSubjectStudentActivity",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClassGroupSubjectStudentActivity_SubmissionId",
                table: "ClassGroupSubjectStudentActivity",
                column: "SubmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassGroupSubjectStudentActivity_Submissions_SubmissionId",
                table: "ClassGroupSubjectStudentActivity",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "SubmissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassGroupSubjectStudentActivity_Submissions_SubmissionId",
                table: "ClassGroupSubjectStudentActivity");

            migrationBuilder.DropIndex(
                name: "IX_ClassGroupSubjectStudentActivity_SubmissionId",
                table: "ClassGroupSubjectStudentActivity");

            migrationBuilder.DropColumn(
                name: "SubmissionId",
                table: "ClassGroupSubjectStudentActivity");
        }
    }
}
