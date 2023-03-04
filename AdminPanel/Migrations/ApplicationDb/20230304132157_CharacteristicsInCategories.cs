using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPanel.Migrations.ApplicationDb
{
    public partial class CharacteristicsInCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubcategoryId",
                table: "Characteristics",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_SubcategoryId",
                table: "Characteristics",
                column: "SubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_Subcategories_SubcategoryId",
                table: "Characteristics",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_Subcategories_SubcategoryId",
                table: "Characteristics");

            migrationBuilder.DropIndex(
                name: "IX_Characteristics_SubcategoryId",
                table: "Characteristics");

            migrationBuilder.DropColumn(
                name: "SubcategoryId",
                table: "Characteristics");
        }
    }
}
