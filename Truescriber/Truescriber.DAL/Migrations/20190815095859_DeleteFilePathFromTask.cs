using Microsoft.EntityFrameworkCore.Migrations;

namespace Truescriber.DAL.Migrations
{
    public partial class DeleteFilePathFromTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Tasks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Tasks",
                nullable: true);
        }
    }
}
