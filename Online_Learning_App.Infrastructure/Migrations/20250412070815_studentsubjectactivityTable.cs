using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class studentsubjectactivityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassGroupSubjectStudentActivity",
                columns: table => new
                {
                    ClassGroupSubjectActivityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassGroupSubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassGroupSubjectStudentActivity", x => x.ClassGroupSubjectActivityId);
                    table.ForeignKey(
                        name: "FK_ClassGroupSubjectStudentActivity_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassGroupSubjectStudentActivity_ClassGroupSubject_ClassGroupSubjectId",
                        column: x => x.ClassGroupSubjectId,
                        principalTable: "ClassGroupSubject",
                        principalColumn: "ClassGroupSubjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassGroupSubjectStudentActivity_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassGroupSubjectStudentActivity_ActivityId",
                table: "ClassGroupSubjectStudentActivity",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassGroupSubjectStudentActivity_ClassGroupSubjectId",
                table: "ClassGroupSubjectStudentActivity",
                column: "ClassGroupSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassGroupSubjectStudentActivity_StudentId",
                table: "ClassGroupSubjectStudentActivity",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassGroupSubjectStudentActivity");
        }
    }
}
