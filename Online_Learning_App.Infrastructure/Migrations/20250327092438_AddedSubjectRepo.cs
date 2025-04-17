using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedSubjectRepo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassGroupSubjectsInformation_ClassGroups_ClassGroupId",
                table: "ClassGroupSubjectsInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassGroupSubjectsInformation_Subjects_SubjectId",
                table: "ClassGroupSubjectsInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassGroupSubjectsInformation",
                table: "ClassGroupSubjectsInformation");

            migrationBuilder.RenameTable(
                name: "ClassGroupSubjectsInformation",
                newName: "ClassGroupSubject");

            migrationBuilder.RenameIndex(
                name: "IX_ClassGroupSubjectsInformation_SubjectId",
                table: "ClassGroupSubject",
                newName: "IX_ClassGroupSubject_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassGroupSubjectsInformation_ClassGroupId",
                table: "ClassGroupSubject",
                newName: "IX_ClassGroupSubject_ClassGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassGroupSubject",
                table: "ClassGroupSubject",
                column: "ClassGroupSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassGroupSubject_ClassGroups_ClassGroupId",
                table: "ClassGroupSubject",
                column: "ClassGroupId",
                principalTable: "ClassGroups",
                principalColumn: "ClassGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassGroupSubject_Subjects_SubjectId",
                table: "ClassGroupSubject",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassGroupSubject_ClassGroups_ClassGroupId",
                table: "ClassGroupSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassGroupSubject_Subjects_SubjectId",
                table: "ClassGroupSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassGroupSubject",
                table: "ClassGroupSubject");

            migrationBuilder.RenameTable(
                name: "ClassGroupSubject",
                newName: "ClassGroupSubjectsInformation");

            migrationBuilder.RenameIndex(
                name: "IX_ClassGroupSubject_SubjectId",
                table: "ClassGroupSubjectsInformation",
                newName: "IX_ClassGroupSubjectsInformation_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassGroupSubject_ClassGroupId",
                table: "ClassGroupSubjectsInformation",
                newName: "IX_ClassGroupSubjectsInformation_ClassGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassGroupSubjectsInformation",
                table: "ClassGroupSubjectsInformation",
                column: "ClassGroupSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassGroupSubjectsInformation_ClassGroups_ClassGroupId",
                table: "ClassGroupSubjectsInformation",
                column: "ClassGroupId",
                principalTable: "ClassGroups",
                principalColumn: "ClassGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassGroupSubjectsInformation_Subjects_SubjectId",
                table: "ClassGroupSubjectsInformation",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
