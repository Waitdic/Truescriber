using Microsoft.EntityFrameworkCore.Migrations;

namespace Truescriber.WEB.Migrations
{
    public partial class ModifyUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Onine",
                table: "AspNetUsers",
                newName: "Online");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Online",
                table: "AspNetUsers",
                newName: "Onine");
        }
    }
}
