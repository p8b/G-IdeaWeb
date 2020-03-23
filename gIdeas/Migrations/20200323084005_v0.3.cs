using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace gIdeas.Migrations
{
    public partial class v03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessClaims_gUsers_UserId",
                table: "AccessClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_gUsers_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_FlaggedIdeas_gUsers_UsersId",
                table: "FlaggedIdeas");

            migrationBuilder.DropForeignKey(
                name: "FK_gUsers_Departments_DepartmentId",
                table: "gUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_gUsers_Roles_RoleId",
                table: "gUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Ideas_gUsers_AuthorId",
                table: "Ideas");

            migrationBuilder.DropForeignKey(
                name: "FK_LoginRecords_gUsers_UserId",
                table: "LoginRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_gUsers",
                table: "gUsers");

            migrationBuilder.RenameTable(
                name: "gUsers",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_gUsers_RoleId",
                table: "Users",
                newName: "IX_Users_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_gUsers_DepartmentId",
                table: "Users",
                newName: "IX_Users_DepartmentId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ClosureDates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstClosure = table.Column<int>(nullable: false),
                    FinalClosure = table.Column<int>(nullable: false),
                    TimeStampLastModified = table.Column<string>(type: "nvarchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosureDates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PageViews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PageName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PageCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageViews", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AccessClaims_Users_UserId",
                table: "AccessClaims",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlaggedIdeas_Users_UsersId",
                table: "FlaggedIdeas",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ideas_Users_AuthorId",
                table: "Ideas",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoginRecords_Users_UserId",
                table: "LoginRecords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessClaims_Users_UserId",
                table: "AccessClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_FlaggedIdeas_Users_UsersId",
                table: "FlaggedIdeas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ideas_Users_AuthorId",
                table: "Ideas");

            migrationBuilder.DropForeignKey(
                name: "FK_LoginRecords_Users_UserId",
                table: "LoginRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ClosureDates");

            migrationBuilder.DropTable(
                name: "PageViews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "gUsers");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                table: "gUsers",
                newName: "IX_gUsers_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_DepartmentId",
                table: "gUsers",
                newName: "IX_gUsers_DepartmentId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_gUsers",
                table: "gUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessClaims_gUsers_UserId",
                table: "AccessClaims",
                column: "UserId",
                principalTable: "gUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_gUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "gUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlaggedIdeas_gUsers_UsersId",
                table: "FlaggedIdeas",
                column: "UsersId",
                principalTable: "gUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_gUsers_Departments_DepartmentId",
                table: "gUsers",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_gUsers_Roles_RoleId",
                table: "gUsers",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ideas_gUsers_AuthorId",
                table: "Ideas",
                column: "AuthorId",
                principalTable: "gUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoginRecords_gUsers_UserId",
                table: "LoginRecords",
                column: "UserId",
                principalTable: "gUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
