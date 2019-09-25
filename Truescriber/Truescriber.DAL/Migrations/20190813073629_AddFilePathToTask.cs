using Microsoft.EntityFrameworkCore.Migrations;

namespace Truescriber.DAL.Migrations
{
    public partial class AddFilePathToTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Tasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Tasks");
        }
    }
}
