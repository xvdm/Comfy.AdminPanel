using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPanel.Migrations.ApplicationDb
{
    public partial class CategoryImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Subcategories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "MainCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MainCategoryImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainCategoryImages", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SubcategoryImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubcategoryImages", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_ImageId",
                table: "Subcategories",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_MainCategories_ImageId",
                table: "MainCategories",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_MainCategories_MainCategoryImages_ImageId",
                table: "MainCategories",
                column: "ImageId",
                principalTable: "MainCategoryImages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subcategories_SubcategoryImages_ImageId",
                table: "Subcategories",
                column: "ImageId",
                principalTable: "SubcategoryImages",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MainCategories_MainCategoryImages_ImageId",
                table: "MainCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Subcategories_SubcategoryImages_ImageId",
                table: "Subcategories");

            migrationBuilder.DropTable(
                name: "MainCategoryImages");

            migrationBuilder.DropTable(
                name: "SubcategoryImages");

            migrationBuilder.DropIndex(
                name: "IX_Subcategories_ImageId",
                table: "Subcategories");

            migrationBuilder.DropIndex(
                name: "IX_MainCategories_ImageId",
                table: "MainCategories");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Subcategories");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "MainCategories");
        }
    }
}
