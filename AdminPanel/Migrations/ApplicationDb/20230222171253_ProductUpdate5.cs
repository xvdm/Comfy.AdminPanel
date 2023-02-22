using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPanel.Migrations.ApplicationDb
{
    public partial class ProductUpdate5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Code",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 1000000,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1000000)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Code",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 1000000,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1000000)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
        }
    }
}
