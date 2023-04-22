using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPanel.Migrations
{
    public partial class subcategoryFilterUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MainCategoryId",
                table: "SubcategoryFilters",
                type: "int",
                nullable: false,
                defaultValue: 0);
            
            migrationBuilder.CreateIndex(
                name: "IX_SubcategoryFilters_MainCategoryId",
                table: "SubcategoryFilters",
                column: "MainCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubcategoryFilters_MainCategories_MainCategoryId",
                table: "SubcategoryFilters",
                column: "MainCategoryId",
                principalTable: "MainCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }  

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubcategoryFilters_MainCategories_MainCategoryId",
                table: "SubcategoryFilters");

            migrationBuilder.DropIndex(
                name: "IX_SubcategoryFilters_MainCategoryId",
                table: "SubcategoryFilters");

            migrationBuilder.DropColumn(
                name: "MainCategoryId",
                table: "SubcategoryFilters");
        }
    }
}
