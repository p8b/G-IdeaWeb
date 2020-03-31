using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace gIdeas.Migrations
{
    public partial class v10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "gDepartments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gDepartments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "gRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AccessClaim = table.Column<string>(type: "nvarchar(50)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gRoles", x => x.Id);
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
                name: "gUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: false),
                    DepartmentId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_gUsers_gDepartments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "gDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gUsers_gRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "gRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccessClaims",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessClaims", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_AccessClaims_gUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "gUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gIdeas",
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
                    lastLogin = table.Column<string>(nullable: true),
                    currentLogin = table.Column<string>(nullable: true),
                    FK_gUserId = table.Column<int>(nullable: true)
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
                name: "gCategoriesToIdeas",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false),
                    IdeaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gCategoriesToIdeas", x => new { x.CategoryId, x.IdeaId });
                    table.ForeignKey(
                        name: "FK_gCategoriesToIdeas_gCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "gCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gCategoriesToIdeas_gIdeas_IdeaId",
                        column: x => x.IdeaId,
                        principalTable: "gIdeas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: false),
                    AnonComment = table.Column<bool>(nullable: false),
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
                name: "gDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(nullable: false),
                    IdeaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_gDocuments_gIdeas_IdeaId",
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
                    Type = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IdeaId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "gVotes",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    IdeaId = table.Column<int>(nullable: false),
                    Thumb = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gVotes", x => new { x.IdeaId, x.UserId });
                    table.ForeignKey(
                        name: "FK_gVotes_gIdeas_IdeaId",
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
                name: "IX_gCategoriesToIdeas_IdeaId",
                table: "gCategoriesToIdeas",
                column: "IdeaId");

            migrationBuilder.CreateIndex(
                name: "IX_gComments_IdeaId",
                table: "gComments",
                column: "IdeaId");

            migrationBuilder.CreateIndex(
                name: "IX_gDocuments_IdeaId",
                table: "gDocuments",
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

            migrationBuilder.CreateIndex(
                name: "IX_gUsers_DepartmentId",
                table: "gUsers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "gUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_gUsers_RoleId",
                table: "gUsers",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessClaims");

            migrationBuilder.DropTable(
                name: "gBrowserInfos");

            migrationBuilder.DropTable(
                name: "gCategoriesToDepartments");

            migrationBuilder.DropTable(
                name: "gCategoriesToIdeas");

            migrationBuilder.DropTable(
                name: "gComments");

            migrationBuilder.DropTable(
                name: "gDocuments");

            migrationBuilder.DropTable(
                name: "gFlaggedIdeas");

            migrationBuilder.DropTable(
                name: "gLastLogins");

            migrationBuilder.DropTable(
                name: "gVotes");

            migrationBuilder.DropTable(
                name: "gCategories");

            migrationBuilder.DropTable(
                name: "gIdeas");

            migrationBuilder.DropTable(
                name: "gUsers");

            migrationBuilder.DropTable(
                name: "gDepartments");

            migrationBuilder.DropTable(
                name: "gRoles");
        }
    }
}