using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace gIdeas.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "gCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    gDepartmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_gCategories_gDepartments_gDepartmentId",
                        column: x => x.gDepartmentId,
                        principalTable: "gDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "gUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: false),
                    DepartmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_gUser_gCategories_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "gCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_gRoles_gUser_UserId",
                        column: x => x.UserId,
                        principalTable: "gUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_gCategories_gDepartmentId",
                table: "gCategories",
                column: "gDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_gRoles_UserId",
                table: "gRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_gUser_DepartmentId",
                table: "gUser",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "gUser",
                column: "NormalizedEmail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gRoles");

            migrationBuilder.DropTable(
                name: "gUser");

            migrationBuilder.DropTable(
                name: "gCategories");

            migrationBuilder.DropTable(
                name: "gDepartments");
        }
    }
}
