using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class uniqueclassgroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ClassName",
                table: "ClassGroups",
                type: "nvarchar(450)",
                nullable: false,
                collation: "SQL_Latin1_General_CP1_CI_AS",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ClassGroups_ClassName",
                table: "ClassGroups",
                column: "ClassName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClassGroups_ClassName",
                table: "ClassGroups");

            migrationBuilder.AlterColumn<string>(
                name: "ClassName",
                table: "ClassGroups",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldCollation: "SQL_Latin1_General_CP1_CI_AS");
        }
    }
}
