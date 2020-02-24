using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Truescriber.DAL.Migrations
{
    public partial class AddCreateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateTime",
                table: "Tasks",
                newName: "FinishTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Tasks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "FinishTime",
                table: "Tasks",
                newName: "UpdateTime");
        }
    }
}
