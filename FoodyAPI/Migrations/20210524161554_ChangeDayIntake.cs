using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodyAPI.Migrations
{
    public partial class ChangeDayIntake : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DayIntake_Users_userID",
                table: "DayIntake");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_UserID",
                table: "Products");

            migrationBuilder.AddColumn<double>(
                name: "DayCalories",
                table: "DayIntake",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_DayIntake_Users_userID",
                table: "DayIntake",
                column: "userID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_UserID",
                table: "Products",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DayIntake_Users_userID",
                table: "DayIntake");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_UserID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DayCalories",
                table: "DayIntake");

            migrationBuilder.AddForeignKey(
                name: "FK_DayIntake_Users_userID",
                table: "DayIntake",
                column: "userID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_UserID",
                table: "Products",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
