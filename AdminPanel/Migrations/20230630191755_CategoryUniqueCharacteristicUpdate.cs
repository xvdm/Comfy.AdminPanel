using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPanel.Migrations
{
    public partial class CategoryUniqueCharacteristicUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CategoryUniqueCharacteristics_CharacteristicsName",
                table: "CategoryUniqueCharacteristics");

            migrationBuilder.DropIndex(
                name: "IX_CategoryUniqueCharacteristics_CharacteristicsValue",
                table: "CategoryUniqueCharacteristics");

            migrationBuilder.DropColumn(
                name: "CharacteristicsName",
                table: "CategoryUniqueCharacteristics");

            migrationBuilder.DropColumn(
                name: "CharacteristicsValue",
                table: "CategoryUniqueCharacteristics");

            migrationBuilder.AddColumn<int>(
                name: "CharacteristicNameId",
                table: "CategoryUniqueCharacteristics",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CharacteristicValueId",
                table: "CategoryUniqueCharacteristics",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryUniqueCharacteristics_CharacteristicNameId",
                table: "CategoryUniqueCharacteristics",
                column: "CharacteristicNameId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryUniqueCharacteristics_CharacteristicValueId",
                table: "CategoryUniqueCharacteristics",
                column: "CharacteristicValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryUniqueCharacteristics_CharacteristicsNames_Characte~",
                table: "CategoryUniqueCharacteristics",
                column: "CharacteristicNameId",
                principalTable: "CharacteristicsNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryUniqueCharacteristics_CharacteristicsValues_Charact~",
                table: "CategoryUniqueCharacteristics",
                column: "CharacteristicValueId",
                principalTable: "CharacteristicsValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryUniqueCharacteristics_CharacteristicsNames_Characte~",
                table: "CategoryUniqueCharacteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryUniqueCharacteristics_CharacteristicsValues_Charact~",
                table: "CategoryUniqueCharacteristics");

            migrationBuilder.DropIndex(
                name: "IX_CategoryUniqueCharacteristics_CharacteristicNameId",
                table: "CategoryUniqueCharacteristics");

            migrationBuilder.DropIndex(
                name: "IX_CategoryUniqueCharacteristics_CharacteristicValueId",
                table: "CategoryUniqueCharacteristics");

            migrationBuilder.DropColumn(
                name: "CharacteristicNameId",
                table: "CategoryUniqueCharacteristics");

            migrationBuilder.DropColumn(
                name: "CharacteristicValueId",
                table: "CategoryUniqueCharacteristics");

            migrationBuilder.AddColumn<string>(
                name: "CharacteristicsName",
                table: "CategoryUniqueCharacteristics",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CharacteristicsValue",
                table: "CategoryUniqueCharacteristics",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryUniqueCharacteristics_CharacteristicsName",
                table: "CategoryUniqueCharacteristics",
                column: "CharacteristicsName");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryUniqueCharacteristics_CharacteristicsValue",
                table: "CategoryUniqueCharacteristics",
                column: "CharacteristicsValue");
        }
    }
}
