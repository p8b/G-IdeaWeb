using Microsoft.EntityFrameworkCore.Migrations;

namespace gIdeas.Migrations
{
    public partial class v11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseDate",
                table: "Ideas");

            migrationBuilder.DropColumn(
                name: "FinalClosureDate",
                table: "Ideas");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Documents",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "ArrayBufferStringBase64",
                table: "Ideas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClosureDate",
                table: "Ideas",
                type: "nvarchar(30)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstClosureDate",
                table: "Ideas",
                type: "nvarchar(30)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Ideas",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrayBufferStringBase64",
                table: "Ideas");

            migrationBuilder.DropColumn(
                name: "ClosureDate",
                table: "Ideas");

            migrationBuilder.DropColumn(
                name: "FirstClosureDate",
                table: "Ideas");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Ideas");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Documents",
                newName: "Path");

            migrationBuilder.AddColumn<string>(
                name: "CloseDate",
                table: "Ideas",
                type: "nvarchar(30)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FinalClosureDate",
                table: "Ideas",
                type: "nvarchar(30)",
                nullable: false,
                defaultValue: "");
        }
    }
}
