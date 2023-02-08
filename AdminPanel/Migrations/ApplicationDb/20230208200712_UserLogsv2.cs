using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPanel.Migrations.ApplicationDb
{
    public partial class UserLogsv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLogs_AspNetUsers_SubjectUserId1",
                table: "UserLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogs_AspNetUsers_UserId1",
                table: "UserLogs");

            migrationBuilder.DropIndex(
                name: "IX_UserLogs_SubjectUserId1",
                table: "UserLogs");

            migrationBuilder.DropIndex(
                name: "IX_UserLogs_UserId1",
                table: "UserLogs");

            migrationBuilder.DropColumn(
                name: "SubjectUserId1",
                table: "UserLogs");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserLogs");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserLogs",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubjectUserId",
                table: "UserLogs",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogs_SubjectUserId",
                table: "UserLogs",
                column: "SubjectUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogs_UserId",
                table: "UserLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogs_AspNetUsers_SubjectUserId",
                table: "UserLogs",
                column: "SubjectUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogs_AspNetUsers_UserId",
                table: "UserLogs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLogs_AspNetUsers_SubjectUserId",
                table: "UserLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogs_AspNetUsers_UserId",
                table: "UserLogs");

            migrationBuilder.DropIndex(
                name: "IX_UserLogs_SubjectUserId",
                table: "UserLogs");

            migrationBuilder.DropIndex(
                name: "IX_UserLogs_UserId",
                table: "UserLogs");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserLogs",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectUserId",
                table: "UserLogs",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectUserId1",
                table: "UserLogs",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "UserLogs",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogs_SubjectUserId1",
                table: "UserLogs",
                column: "SubjectUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogs_UserId1",
                table: "UserLogs",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogs_AspNetUsers_SubjectUserId1",
                table: "UserLogs",
                column: "SubjectUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogs_AspNetUsers_UserId1",
                table: "UserLogs",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
