using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPanel.Migrations
{
    public partial class showcasesubcategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubcategoryId",
                table: "ShowcaseGroups",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShowcaseGroups_SubcategoryId",
                table: "ShowcaseGroups",
                column: "SubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowcaseGroups_Subcategories_SubcategoryId",
                table: "ShowcaseGroups",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowcaseGroups_Subcategories_SubcategoryId",
                table: "ShowcaseGroups");

            migrationBuilder.DropIndex(
                name: "IX_ShowcaseGroups_SubcategoryId",
                table: "ShowcaseGroups");

            migrationBuilder.DropColumn(
                name: "SubcategoryId",
                table: "ShowcaseGroups");
        }
    }
}
