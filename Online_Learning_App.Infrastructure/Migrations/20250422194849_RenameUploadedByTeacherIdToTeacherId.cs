using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameUploadedByTeacherIdToTeacherId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrintableResources_Teachers_UploadedByTeacherId",
                table: "PrintableResources");

            migrationBuilder.RenameColumn(
                name: "UploadedByTeacherId",
                table: "PrintableResources",
                newName: "TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_PrintableResources_UploadedByTeacherId",
                table: "PrintableResources",
                newName: "IX_PrintableResources_TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrintableResources_Teachers_TeacherId",
                table: "PrintableResources",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrintableResources_Teachers_TeacherId",
                table: "PrintableResources");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "PrintableResources",
                newName: "UploadedByTeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_PrintableResources_TeacherId",
                table: "PrintableResources",
                newName: "IX_PrintableResources_UploadedByTeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrintableResources_Teachers_UploadedByTeacherId",
                table: "PrintableResources",
                column: "UploadedByTeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
