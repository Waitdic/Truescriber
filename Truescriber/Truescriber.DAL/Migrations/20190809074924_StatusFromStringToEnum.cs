using Microsoft.EntityFrameworkCore.Migrations;

namespace Truescriber.DAL.Migrations
{
    public partial class StatusFromStringToEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // NOTE: This command is needed here for
            // working conversion from string to int
            migrationBuilder.Sql(@"
                UPDATE Tasks
                SET Status = '';
            ");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Tasks",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Tasks",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
