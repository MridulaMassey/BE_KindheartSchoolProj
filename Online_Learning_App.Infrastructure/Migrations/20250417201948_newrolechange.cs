using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newrolechange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Activities_ActivityId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_StudentId",
                table: "Submissions");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_StudentId_ActivityId",
                table: "Submissions",
                columns: new[] { "StudentId", "ActivityId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Activities_ActivityId",
                table: "Submissions",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Activities_ActivityId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_StudentId_ActivityId",
                table: "Submissions");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_StudentId",
                table: "Submissions",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Activities_ActivityId",
                table: "Submissions",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
