using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AdminPanel.Migrations
{
    public partial class characteristicGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CharacteristicGroupId",
                table: "Characteristics",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CharacteristicGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacteristicGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacteristicGroups_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_CharacteristicGroupId",
                table: "Characteristics",
                column: "CharacteristicGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicGroups_ProductId",
                table: "CharacteristicGroups",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_CharacteristicGroups_CharacteristicGroupId",
                table: "Characteristics",
                column: "CharacteristicGroupId",
                principalTable: "CharacteristicGroups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_CharacteristicGroups_CharacteristicGroupId",
                table: "Characteristics");

            migrationBuilder.DropTable(
                name: "CharacteristicGroups");

            migrationBuilder.DropIndex(
                name: "IX_Characteristics_CharacteristicGroupId",
                table: "Characteristics");

            migrationBuilder.DropColumn(
                name: "CharacteristicGroupId",
                table: "Characteristics");
        }
    }
}
