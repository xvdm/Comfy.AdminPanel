using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AdminPanel.Migrations
{
    public partial class categoriesImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Subcategories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "MainCategories",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Subcategories");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "MainCategories");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Subcategories",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "MainCategories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MainCategoryImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainCategoryImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubcategoryImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubcategoryImages", x => x.Id);
                });

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
    }
}
