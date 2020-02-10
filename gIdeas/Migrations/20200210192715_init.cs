using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace gIdeas.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "gCategoriesToDepartments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DepartmentId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gCategoriesToDepartments", x => x.Id);
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

            migrationBuilder.CreateIndex(
                name: "IX_gCategoriesToDepartments_CategoryId",
                table: "gCategoriesToDepartments",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_gCategoriesToDepartments_DepartmentId",
                table: "gCategoriesToDepartments",
                column: "DepartmentId");

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
                name: "gCategoriesToDepartments");

            migrationBuilder.DropTable(
                name: "gUsers");

            migrationBuilder.DropTable(
                name: "gCategories");

            migrationBuilder.DropTable(
                name: "gDepartments");

            migrationBuilder.DropTable(
                name: "gRoles");
        }
    }
}
