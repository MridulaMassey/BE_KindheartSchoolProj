using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class studentsubjectactivityTablePrimaryAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClassGroupSubjectActivityId",
                table: "ClassGroupSubjectStudentActivity",
                newName: "ClassGroupSubjectStudentActivityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClassGroupSubjectStudentActivityId",
                table: "ClassGroupSubjectStudentActivity",
                newName: "ClassGroupSubjectActivityId");
        }
    }
}
