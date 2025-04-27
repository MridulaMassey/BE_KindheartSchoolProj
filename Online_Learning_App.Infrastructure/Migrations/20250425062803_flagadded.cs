using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class flagadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "ClassGroupSubjectStudentActivity",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "ClassGroupSubjectStudentActivity");
        }
    }
}
