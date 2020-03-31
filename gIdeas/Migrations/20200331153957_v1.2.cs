using Microsoft.EntityFrameworkCore.Migrations;

namespace gIdeas.Migrations
{
    public partial class v12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlaggedIdeas_Users_UsersId",
                table: "FlaggedIdeas");

            migrationBuilder.DropForeignKey(
                name: "FK_LoginRecords_Users_UserId",
                table: "LoginRecords");

            migrationBuilder.DropColumn(
                name: "ArrayBufferStringBase64",
                table: "Ideas");

            migrationBuilder.RenameColumn(
                name: "DisplayAnonymous",
                table: "Ideas",
                newName: "IsBlocked");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "FlaggedIdeas",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FlaggedIdeas_UsersId",
                table: "FlaggedIdeas",
                newName: "IX_FlaggedIdeas_UserId");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Comments",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ClosureDates",
                newName: "Year");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "LoginRecords",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAnonymous",
                table: "Ideas",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "FlaggedIdeas",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BlobStringBase64",
                table: "Documents",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_FlaggedIdeas_Users_UserId",
                table: "FlaggedIdeas",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoginRecords_Users_UserId",
                table: "LoginRecords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlaggedIdeas_Users_UserId",
                table: "FlaggedIdeas");

            migrationBuilder.DropForeignKey(
                name: "FK_LoginRecords_Users_UserId",
                table: "LoginRecords");

            migrationBuilder.DropColumn(
                name: "IsAnonymous",
                table: "Ideas");

            migrationBuilder.DropColumn(
                name: "BlobStringBase64",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "IsBlocked",
                table: "Ideas",
                newName: "DisplayAnonymous");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FlaggedIdeas",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_FlaggedIdeas_UserId",
                table: "FlaggedIdeas",
                newName: "IX_FlaggedIdeas_UsersId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Comments",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "ClosureDates",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "LoginRecords",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "ArrayBufferStringBase64",
                table: "Ideas",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "FlaggedIdeas",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_FlaggedIdeas_Users_UsersId",
                table: "FlaggedIdeas",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoginRecords_Users_UserId",
                table: "LoginRecords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
