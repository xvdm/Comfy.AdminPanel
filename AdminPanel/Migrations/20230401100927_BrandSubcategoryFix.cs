using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPanel.Migrations
{
    public partial class BrandSubcategoryFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "BrandSubcategory",
                columns: table => new
                {
                    SubcategoriesId = table.Column<int>(type: "int", nullable: false),
                    UniqueBrandsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandSubcategory", x => new { x.SubcategoriesId, x.UniqueBrandsId });
                    table.ForeignKey(
                        name: "FK_BrandSubcategory_Brands_UniqueBrandsId",
                        column: x => x.UniqueBrandsId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandSubcategory_Subcategories_SubcategoriesId",
                        column: x => x.SubcategoriesId,
                        principalTable: "Subcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BrandSubcategory_UniqueBrandsId",
                table: "BrandSubcategory",
                column: "UniqueBrandsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrandSubcategory");

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
    }
}
