using Microsoft.EntityFrameworkCore.Migrations;

namespace Truescriber.DAL.Migrations
{
    public partial class AddDurationMoreMinute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DurationMoreMinute",
                table: "Tasks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationMoreMinute",
                table: "Tasks");
        }
    }
}
