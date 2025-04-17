using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClassGroupbasedtableahf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_ClassGroups_ClassGroupId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_ClassGroups_ClassGroupId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Activities_ActivityId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Students_StudentId",
                table: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Activities",
                table: "Activities");

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId1",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ClassGroupId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClassGroupId",
                table: "Activities",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Activities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ActivityName",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activities",
                table: "Activities",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId1",
                table: "Users",
                column: "RoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_ClassGroups_ClassGroupId",
                table: "Activities",
                column: "ClassGroupId",
                principalTable: "ClassGroups",
                principalColumn: "ClassGroupId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_ClassGroups_ClassGroupId",
                table: "Students",
                column: "ClassGroupId",
                principalTable: "ClassGroups",
                principalColumn: "ClassGroupId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Activities_ActivityId",
                table: "Submissions",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Students_StudentId",
                table: "Submissions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId1",
                table: "Users",
                column: "RoleId1",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_ClassGroups_ClassGroupId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_ClassGroups_ClassGroupId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Activities_ActivityId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Students_StudentId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId1",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId1",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Activities",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ActivityName",
                table: "Activities");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClassGroupId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ClassGroupId",
                table: "Activities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activities",
                table: "Activities",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_ClassGroups_ClassGroupId",
                table: "Activities",
                column: "ClassGroupId",
                principalTable: "ClassGroups",
                principalColumn: "ClassGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_ClassGroups_ClassGroupId",
                table: "Students",
                column: "ClassGroupId",
                principalTable: "ClassGroups",
                principalColumn: "ClassGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Activities_ActivityId",
                table: "Submissions",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Students_StudentId",
                table: "Submissions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
