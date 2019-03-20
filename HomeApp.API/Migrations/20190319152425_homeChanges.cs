using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeApp.API.Migrations
{
    public partial class homeChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Homes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Homes");
        }
    }
}
