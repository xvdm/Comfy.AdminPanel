using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPanel.Migrations.ApplicationDb
{
    public partial class UniqueBrandsInCategoryFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_Subcategories_SubcategoryId1",
                table: "Characteristics");

            migrationBuilder.DropIndex(
                name: "IX_Characteristics_SubcategoryId1",
                table: "Characteristics");

            migrationBuilder.DropColumn(
                name: "SubcategoryId1",
                table: "Characteristics");

            migrationBuilder.AddColumn<int>(
                name: "SubcategoryId",
                table: "Brands",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brands_SubcategoryId",
                table: "Brands",
                column: "SubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Subcategories_SubcategoryId",
                table: "Brands",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Subcategories_SubcategoryId",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Brands_SubcategoryId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "SubcategoryId",
                table: "Brands");

            migrationBuilder.AddColumn<int>(
                name: "SubcategoryId1",
                table: "Characteristics",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_SubcategoryId1",
                table: "Characteristics",
                column: "SubcategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_Subcategories_SubcategoryId1",
                table: "Characteristics",
                column: "SubcategoryId1",
                principalTable: "Subcategories",
                principalColumn: "Id");
        }
    }
}
