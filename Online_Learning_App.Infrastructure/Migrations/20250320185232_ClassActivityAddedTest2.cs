using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClassActivityAddedTest2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId",
                table: "Activities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "WeightagePercent",
                table: "Activities",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_SubjectId",
                table: "Activities",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Subjects_SubjectId",
                table: "Activities",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Subjects_SubjectId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_SubjectId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "WeightagePercent",
                table: "Activities");
        }
    }
}
