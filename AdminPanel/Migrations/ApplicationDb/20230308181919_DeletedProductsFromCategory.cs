using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPanel.Migrations.ApplicationDb
{
    public partial class DeletedProductsFromCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Subcategories_CategoryId",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Subcategories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Subcategories_CategoryId",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Subcategories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id");
        }
    }
}
