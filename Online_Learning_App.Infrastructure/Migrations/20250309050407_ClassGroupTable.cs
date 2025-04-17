using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClassGroupTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Teachers_TeacherId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Teachers_TeacherId1",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Activities_ActivityId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Students_StudentId1",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_StudentId1",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Activities_TeacherId1",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "TeacherId1",
                table: "Activities");

            migrationBuilder.AddColumn<Guid>(
                name: "ClassGroupId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ClassGroupId",
                table: "Activities",
                type: "uniqueidentifier",
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ClassGroups",
                columns: table => new
                {
                    ClassGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassGroups", x => x.ClassGroupId);
                    table.ForeignKey(
                        name: "FK_ClassGroups_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassGroupId",
                table: "Students",
                column: "ClassGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ClassGroupId",
                table: "Activities",
                column: "ClassGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassGroups_TeacherId",
                table: "ClassGroups",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_ClassGroups_ClassGroupId",
                table: "Activities",
                column: "ClassGroupId",
                principalTable: "ClassGroups",
                principalColumn: "ClassGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Teachers_TeacherId",
                table: "Activities",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_ClassGroups_ClassGroupId",
                table: "Students",
                column: "ClassGroupId",
                principalTable: "ClassGroups",
                principalColumn: "ClassGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Activities_ActivityId",
                table: "Submissions",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "ActivityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_ClassGroups_ClassGroupId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Teachers_TeacherId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_ClassGroups_ClassGroupId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Activities_ActivityId",
                table: "Submissions");

            migrationBuilder.DropTable(
                name: "ClassGroups");

            migrationBuilder.DropIndex(
                name: "IX_Students_ClassGroupId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Activities_ClassGroupId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ClassGroupId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ClassGroupId",
                table: "Activities");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId1",
                table: "Submissions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId1",
                table: "Activities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_StudentId1",
                table: "Submissions",
                column: "StudentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_TeacherId1",
                table: "Activities",
                column: "TeacherId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Teachers_TeacherId",
                table: "Activities",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Teachers_TeacherId1",
                table: "Activities",
                column: "TeacherId1",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Activities_ActivityId",
                table: "Submissions",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Students_StudentId1",
                table: "Submissions",
                column: "StudentId1",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
