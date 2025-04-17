using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class subjectgradeaddedformigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    GradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinMarks = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxMarks = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.GradeId);
                });

            migrationBuilder.CreateTable(
                name: "ClassGroupSubjectGrade",
                columns: table => new
                {
                    ClassGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ObtainedMarks = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassGroupSubjectGrade", x => new { x.ClassGroupId, x.SubjectId, x.GradeId });
                    table.ForeignKey(
                        name: "FK_ClassGroupSubjectGrade_ClassGroups_ClassGroupId",
                        column: x => x.ClassGroupId,
                        principalTable: "ClassGroups",
                        principalColumn: "ClassGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassGroupSubjectGrade_Grade_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grade",
                        principalColumn: "GradeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassGroupSubjectGrade_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectGrade",
                columns: table => new
                {
                    SubjectGradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsOverallGrade = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectGrade", x => x.SubjectGradeId);
                    table.ForeignKey(
                        name: "FK_SubjectGrade_Grade_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grade",
                        principalColumn: "GradeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectGrade_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassGroupSubjectGrade_GradeId",
                table: "ClassGroupSubjectGrade",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassGroupSubjectGrade_SubjectId",
                table: "ClassGroupSubjectGrade",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGrade_GradeId",
                table: "SubjectGrade",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGrade_SubjectId",
                table: "SubjectGrade",
                column: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassGroupSubjectGrade");

            migrationBuilder.DropTable(
                name: "SubjectGrade");

            migrationBuilder.DropTable(
                name: "Grade");
        }
    }
}
