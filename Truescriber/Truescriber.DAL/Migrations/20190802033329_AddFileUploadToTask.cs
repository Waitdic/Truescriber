using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Truescriber.DAL.Migrations
{
    public partial class AddFileUploadToTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Link",
                table: "Tasks",
                newName: "TaskName");

            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "Tasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "TaskName",
                table: "Tasks",
                newName: "Link");
        }
    }
}
