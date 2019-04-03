using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeApp.API.Migrations
{
    public partial class changedUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
