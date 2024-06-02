using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogInOutAPI.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "UserId");

            migrationBuilder.CreateTable(
                name: "UserCompany",
                columns: table => new
                {
                    UserCompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCompany", x => x.UserCompanyId);
                });

            migrationBuilder.CreateTable(
                name: "UserCompanyUsers",
                columns: table => new
                {
                    UserCompaniesUserCompanyId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCompanyUsers", x => new { x.UserCompaniesUserCompanyId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_UserCompanyUsers_UserCompany_UserCompaniesUserCompanyId",
                        column: x => x.UserCompaniesUserCompanyId,
                        principalTable: "UserCompany",
                        principalColumn: "UserCompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCompanyUsers_users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCompanyUsers_UsersUserId",
                table: "UserCompanyUsers",
                column: "UsersUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCompanyUsers");

            migrationBuilder.DropTable(
                name: "UserCompany");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "users",
                newName: "Id");
        }
    }
}
