using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPanel.Migrations
{
    public partial class ProductCharacteristicObjectCycleErrorFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_Products_ProductId",
                table: "Characteristics");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_Products_ProductId",
                table: "Characteristics",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_Products_ProductId",
                table: "Characteristics");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_Products_ProductId",
                table: "Characteristics",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
