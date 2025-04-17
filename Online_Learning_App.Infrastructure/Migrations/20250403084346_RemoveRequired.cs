using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassGroupSubjectActivities",
                columns: table => new
                {
                    ClassGroupSubjectActivityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassGroupSubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassGroupSubjectActivities", x => x.ClassGroupSubjectActivityId);
                    table.ForeignKey(
                        name: "FK_ClassGroupSubjectActivities_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassGroupSubjectActivities_ClassGroupSubject_ClassGroupSubjectId",
                        column: x => x.ClassGroupSubjectId,
                        principalTable: "ClassGroupSubject",
                        principalColumn: "ClassGroupSubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassGroupSubjectActivities_ActivityId",
                table: "ClassGroupSubjectActivities",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassGroupSubjectActivities_ClassGroupSubjectId",
                table: "ClassGroupSubjectActivities",
                column: "ClassGroupSubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassGroupSubjectActivities");
        }
    }
}
