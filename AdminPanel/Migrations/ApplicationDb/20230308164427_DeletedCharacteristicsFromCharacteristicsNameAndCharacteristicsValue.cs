using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPanel.Migrations.ApplicationDb
{
    public partial class DeletedCharacteristicsFromCharacteristicsNameAndCharacteristicsValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_CharacteristicsNames_CharacteristicsNameId",
                table: "Characteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_CharacteristicsValues_CharacteristicsValueId",
                table: "Characteristics");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_CharacteristicsNames_CharacteristicsNameId",
                table: "Characteristics",
                column: "CharacteristicsNameId",
                principalTable: "CharacteristicsNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_CharacteristicsValues_CharacteristicsValueId",
                table: "Characteristics",
                column: "CharacteristicsValueId",
                principalTable: "CharacteristicsValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_CharacteristicsNames_CharacteristicsNameId",
                table: "Characteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_CharacteristicsValues_CharacteristicsValueId",
                table: "Characteristics");

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
    }
}
