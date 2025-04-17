using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassGroupSubject_ClassGroups_ClassGroupId",
                table: "ClassGroupSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassGroupSubject_Subject_SubjectId",
                table: "ClassGroupSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subject",
                table: "Subject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassGroupSubject",
                table: "ClassGroupSubject");

            migrationBuilder.RenameTable(
                name: "Subject",
                newName: "Subjects");

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

            migrationBuilder.AddColumn<Guid>(
                name: "AdminId",
                table: "ClassGroups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects",
                column: "SubjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassGroupSubjectsInformation",
                table: "ClassGroupSubjectsInformation",
                column: "ClassGroupSubjectId");

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    AdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.AdminId);
                    table.ForeignKey(
                        name: "FK_Admin_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Admin_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassGroups_AdminId",
                table: "ClassGroups",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Admin_RoleId",
                table: "Admin",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Admin_UserId",
                table: "Admin",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassGroups_Admin_AdminId",
                table: "ClassGroups",
                column: "AdminId",
                principalTable: "Admin",
                principalColumn: "AdminId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassGroups_Admin_AdminId",
                table: "ClassGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassGroupSubjectsInformation_ClassGroups_ClassGroupId",
                table: "ClassGroupSubjectsInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassGroupSubjectsInformation_Subjects_SubjectId",
                table: "ClassGroupSubjectsInformation");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropIndex(
                name: "IX_ClassGroups_AdminId",
                table: "ClassGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassGroupSubjectsInformation",
                table: "ClassGroupSubjectsInformation");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "ClassGroups");

            migrationBuilder.RenameTable(
                name: "Subjects",
                newName: "Subject");

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
                name: "PK_Subject",
                table: "Subject",
                column: "SubjectId");

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
                name: "FK_ClassGroupSubject_Subject_SubjectId",
                table: "ClassGroupSubject",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
