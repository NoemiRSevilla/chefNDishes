using Microsoft.EntityFrameworkCore.Migrations;

namespace chefNDishes.Migrations
{
    public partial class MoreMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chef",
                table: "Dishes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Chef",
                table: "Dishes",
                nullable: false,
                defaultValue: "");
        }
    }
}
