using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GradeChangesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MaxMarks",
                table: "SubjectGrade",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxMarks",
                table: "Grade",
                type: "decimal(5,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId1",
                table: "Activities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ActivityGrade",
                columns: table => new
                {
                    ActivityGradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityGrade", x => x.ActivityGradeId);
                    table.ForeignKey(
                        name: "FK_ActivityGrade_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityGrade_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinalGrade",
                columns: table => new
                {
                    FinalGradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinalScore = table.Column<double>(type: "float", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReleased = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalGrade", x => x.FinalGradeId);
                    table.ForeignKey(
                        name: "FK_FinalGrade_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinalGrade_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_SubjectId1",
                table: "Activities",
                column: "SubjectId1");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityGrade_ActivityId",
                table: "ActivityGrade",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityGrade_StudentId",
                table: "ActivityGrade",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalGrade_StudentId",
                table: "FinalGrade",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalGrade_SubjectId",
                table: "FinalGrade",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Subjects_SubjectId1",
                table: "Activities",
                column: "SubjectId1",
                principalTable: "Subjects",
                principalColumn: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Subjects_SubjectId1",
                table: "Activities");

            migrationBuilder.DropTable(
                name: "ActivityGrade");

            migrationBuilder.DropTable(
                name: "FinalGrade");

            migrationBuilder.DropIndex(
                name: "IX_Activities_SubjectId1",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "MaxMarks",
                table: "SubjectGrade");

            migrationBuilder.DropColumn(
                name: "SubjectId1",
                table: "Activities");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxMarks",
                table: "Grade",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 10,
                oldScale: 2);
        }
    }
}
