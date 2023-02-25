using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPanel.Migrations.ApplicationDb
{
    public partial class FixForCharacteristicsId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_CharacteristicsNames",
                table: "Characteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_CharacteristicsValues",
                table: "Characteristics");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CharacteristicsValues",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CharacteristicsNames",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_CharacteristicsNames_CharacteristicsNameId",
                table: "Characteristics",
                column: "CharacteristicsNameId",
                principalTable: "CharacteristicsNames",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_CharacteristicsValues_CharacteristicsValueId",
                table: "Characteristics",
                column: "CharacteristicsValueId",
                principalTable: "CharacteristicsValues",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_CharacteristicsNames_CharacteristicsNameId",
                table: "Characteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_CharacteristicsValues_CharacteristicsValueId",
                table: "Characteristics");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CharacteristicsValues",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CharacteristicsNames",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_CharacteristicsNames",
                table: "Characteristics",
                column: "CharacteristicsNameId",
                principalTable: "CharacteristicsNames",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_CharacteristicsValues",
                table: "Characteristics",
                column: "CharacteristicsValueId",
                principalTable: "CharacteristicsValues",
                principalColumn: "Id");
        }
    }
}
