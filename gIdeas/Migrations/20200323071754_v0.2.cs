using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace gIdeas.Migrations
{
    public partial class v02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gCategoriesToIdeas_gCategories_CategoryId",
                table: "gCategoriesToIdeas");

            migrationBuilder.DropForeignKey(
                name: "FK_gCategoriesToIdeas_gIdeas_IdeaId",
                table: "gCategoriesToIdeas");

            migrationBuilder.DropForeignKey(
                name: "FK_gDocuments_gIdeas_IdeaId",
                table: "gDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_gUsers_gDepartments_DepartmentId",
                table: "gUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_gUsers_gRoles_RoleId",
                table: "gUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_gVotes_gIdeas_IdeaId",
                table: "gVotes");

            migrationBuilder.DropTable(
                name: "gBrowserInfos");

            migrationBuilder.DropTable(
                name: "gCategoriesToDepartments");

            migrationBuilder.DropTable(
                name: "gComments");

            migrationBuilder.DropTable(
                name: "gFlaggedIdeas");

            migrationBuilder.DropTable(
                name: "gLastLogins");

            migrationBuilder.DropTable(
                name: "gCategories");

            migrationBuilder.DropTable(
                name: "gIdeas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_gVotes",
                table: "gVotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_gRoles",
                table: "gRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_gDocuments",
                table: "gDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_gDepartments",
                table: "gDepartments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_gCategoriesToIdeas",
                table: "gCategoriesToIdeas");

            migrationBuilder.RenameTable(
                name: "gVotes",
                newName: "Votes");

            migrationBuilder.RenameTable(
                name: "gRoles",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "gDocuments",
                newName: "Documents");

            migrationBuilder.RenameTable(
                name: "gDepartments",
                newName: "Departments");

            migrationBuilder.RenameTable(
                name: "gCategoriesToIdeas",
                newName: "CategoriesToIdeas");

            migrationBuilder.RenameIndex(
                name: "IX_gDocuments_IdeaId",
                table: "Documents",
                newName: "IX_Documents_IdeaId");

            migrationBuilder.RenameIndex(
                name: "IX_gCategoriesToIdeas_IdeaId",
                table: "CategoriesToIdeas",
                newName: "IX_CategoriesToIdeas_IdeaId");

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "gUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Votes",
                table: "Votes",
                columns: new[] { "IdeaId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documents",
                table: "Documents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriesToIdeas",
                table: "CategoriesToIdeas",
                columns: new[] { "CategoryId", "IdeaId" });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ideas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    ShortDescription = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    CloseDate = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    FinalClosureDate = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    DisplayAnonymous = table.Column<bool>(nullable: false),
                    AuthorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ideas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ideas_gUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "gUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoginRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeStamp = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    BrowserName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginRecords_gUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "gUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: false),
                    IsAnonymous = table.Column<bool>(nullable: false),
                    IdeaId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    SubmissionDate = table.Column<string>(type: "nvarchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Ideas_IdeaId",
                        column: x => x.IdeaId,
                        principalTable: "Ideas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_gUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "gUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlaggedIdeas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IdeaId = table.Column<int>(nullable: false),
                    UsersId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlaggedIdeas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlaggedIdeas_Ideas_IdeaId",
                        column: x => x.IdeaId,
                        principalTable: "Ideas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlaggedIdeas_gUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "gUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IdeaId",
                table: "Comments",
                column: "IdeaId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FlaggedIdeas_IdeaId",
                table: "FlaggedIdeas",
                column: "IdeaId");

            migrationBuilder.CreateIndex(
                name: "IX_FlaggedIdeas_UsersId",
                table: "FlaggedIdeas",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Ideas_AuthorId",
                table: "Ideas",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginRecords_UserId",
                table: "LoginRecords",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriesToIdeas_Categories_CategoryId",
                table: "CategoriesToIdeas",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriesToIdeas_Ideas_IdeaId",
                table: "CategoriesToIdeas",
                column: "IdeaId",
                principalTable: "Ideas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Ideas_IdeaId",
                table: "Documents",
                column: "IdeaId",
                principalTable: "Ideas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Votes_Ideas_IdeaId",
                table: "Votes",
                column: "IdeaId",
                principalTable: "Ideas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoriesToIdeas_Categories_CategoryId",
                table: "CategoriesToIdeas");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoriesToIdeas_Ideas_IdeaId",
                table: "CategoriesToIdeas");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Ideas_IdeaId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_gUsers_Departments_DepartmentId",
                table: "gUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_gUsers_Roles_RoleId",
                table: "gUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Ideas_IdeaId",
                table: "Votes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "FlaggedIdeas");

            migrationBuilder.DropTable(
                name: "LoginRecords");

            migrationBuilder.DropTable(
                name: "Ideas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Votes",
                table: "Votes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documents",
                table: "Documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriesToIdeas",
                table: "CategoriesToIdeas");

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "gUsers");

            migrationBuilder.RenameTable(
                name: "Votes",
                newName: "gVotes");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "gRoles");

            migrationBuilder.RenameTable(
                name: "Documents",
                newName: "gDocuments");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "gDepartments");

            migrationBuilder.RenameTable(
                name: "CategoriesToIdeas",
                newName: "gCategoriesToIdeas");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_IdeaId",
                table: "gDocuments",
                newName: "IX_gDocuments_IdeaId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoriesToIdeas_IdeaId",
                table: "gCategoriesToIdeas",
                newName: "IX_gCategoriesToIdeas_IdeaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_gVotes",
                table: "gVotes",
                columns: new[] { "IdeaId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_gRoles",
                table: "gRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_gDocuments",
                table: "gDocuments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_gDepartments",
                table: "gDepartments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_gCategoriesToIdeas",
                table: "gCategoriesToIdeas",
                columns: new[] { "CategoryId", "IdeaId" });

            migrationBuilder.CreateTable(
                name: "gBrowserInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrowserName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gBrowserInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "gCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    ShortDescription = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "gIdeas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CloseDate = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    CreatedDate = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    DisplayAnonymous = table.Column<bool>(nullable: false),
                    FinalClosureDate = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    ShortDescription = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gIdeas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_gIdeas_gUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "gUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gLastLogins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FK_gUserId = table.Column<int>(nullable: true),
                    currentLogin = table.Column<string>(nullable: true),
                    lastLogin = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gLastLogins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_gLastLogins_gUsers_FK_gUserId",
                        column: x => x.FK_gUserId,
                        principalTable: "gUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "gCategoriesToDepartments",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gCategoriesToDepartments", x => new { x.CategoryId, x.DepartmentId });
                    table.ForeignKey(
                        name: "FK_gCategoriesToDepartments_gCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "gCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gCategoriesToDepartments_gDepartments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "gDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnonComment = table.Column<bool>(nullable: false),
                    Comment = table.Column<string>(nullable: false),
                    IdeaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_gComments_gIdeas_IdeaId",
                        column: x => x.IdeaId,
                        principalTable: "gIdeas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gFlaggedIdeas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    IdeaId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gFlaggedIdeas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_gFlaggedIdeas_gIdeas_IdeaId",
                        column: x => x.IdeaId,
                        principalTable: "gIdeas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_gCategoriesToDepartments_DepartmentId",
                table: "gCategoriesToDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_gComments_IdeaId",
                table: "gComments",
                column: "IdeaId");

            migrationBuilder.CreateIndex(
                name: "IX_gFlaggedIdeas_IdeaId",
                table: "gFlaggedIdeas",
                column: "IdeaId");

            migrationBuilder.CreateIndex(
                name: "IX_gIdeas_UserId",
                table: "gIdeas",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_gLastLogins_FK_gUserId",
                table: "gLastLogins",
                column: "FK_gUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_gCategoriesToIdeas_gCategories_CategoryId",
                table: "gCategoriesToIdeas",
                column: "CategoryId",
                principalTable: "gCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_gCategoriesToIdeas_gIdeas_IdeaId",
                table: "gCategoriesToIdeas",
                column: "IdeaId",
                principalTable: "gIdeas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_gDocuments_gIdeas_IdeaId",
                table: "gDocuments",
                column: "IdeaId",
                principalTable: "gIdeas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_gUsers_gDepartments_DepartmentId",
                table: "gUsers",
                column: "DepartmentId",
                principalTable: "gDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_gUsers_gRoles_RoleId",
                table: "gUsers",
                column: "RoleId",
                principalTable: "gRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_gVotes_gIdeas_IdeaId",
                table: "gVotes",
                column: "IdeaId",
                principalTable: "gIdeas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
