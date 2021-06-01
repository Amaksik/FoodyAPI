using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodyAPI.Migrations
{
    public partial class ChengeProductModelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photoid",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Productid",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photoid",
                table: "Products",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Productid",
                table: "Products",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
