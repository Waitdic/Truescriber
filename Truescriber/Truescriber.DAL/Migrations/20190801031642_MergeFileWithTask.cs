using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Truescriber.DAL.Migrations
{
    public partial class MergeFileWithTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Format",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Length",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Tasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Format",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "Tasks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(nullable: true),
                    Format = table.Column<string>(nullable: true),
                    Length = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    TaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_TaskId",
                table: "Files",
                column: "TaskId");
        }
    }
}
