using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AdminPanel.Migrations
{
    public partial class CategoryUniqueCharacteristic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_CharacteristicGroups_CharacteristicGroupId",
                table: "Characteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_Subcategories_SubcategoryId",
                table: "Characteristics");

            migrationBuilder.DropIndex(
                name: "IX_Characteristics_SubcategoryId",
                table: "Characteristics");

            migrationBuilder.DropColumn(
                name: "SubcategoryId",
                table: "Characteristics");

            migrationBuilder.AlterColumn<int>(
                name: "CharacteristicGroupId",
                table: "Characteristics",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CategoryUniqueCharacteristics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubcategoryId = table.Column<int>(type: "integer", nullable: false),
                    CharacteristicsName = table.Column<string>(type: "text", nullable: false),
                    CharacteristicsValue = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryUniqueCharacteristics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryUniqueCharacteristics_Subcategories_SubcategoryId",
                        column: x => x.SubcategoryId,
                        principalTable: "Subcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryUniqueCharacteristics_CharacteristicsName",
                table: "CategoryUniqueCharacteristics",
                column: "CharacteristicsName");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryUniqueCharacteristics_CharacteristicsValue",
                table: "CategoryUniqueCharacteristics",
                column: "CharacteristicsValue");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryUniqueCharacteristics_SubcategoryId",
                table: "CategoryUniqueCharacteristics",
                column: "SubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_CharacteristicGroups_CharacteristicGroupId",
                table: "Characteristics",
                column: "CharacteristicGroupId",
                principalTable: "CharacteristicGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_CharacteristicGroups_CharacteristicGroupId",
                table: "Characteristics");

            migrationBuilder.DropTable(
                name: "CategoryUniqueCharacteristics");

            migrationBuilder.AlterColumn<int>(
                name: "CharacteristicGroupId",
                table: "Characteristics",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "SubcategoryId",
                table: "Characteristics",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_SubcategoryId",
                table: "Characteristics",
                column: "SubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_CharacteristicGroups_CharacteristicGroupId",
                table: "Characteristics",
                column: "CharacteristicGroupId",
                principalTable: "CharacteristicGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_Subcategories_SubcategoryId",
                table: "Characteristics",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id");
        }
    }
}
