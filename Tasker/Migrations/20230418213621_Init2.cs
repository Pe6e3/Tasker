using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasker.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Roles_RolesRoleId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "RolesRoleId",
                table: "User",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_User_RolesRoleId",
                table: "User",
                newName: "IX_User_RoleId");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_CategoryId",
                table: "User",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Category_CategoryId",
                table: "User",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Roles_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Category_CategoryId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Roles_RoleId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_CategoryId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "User",
                newName: "RolesRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_User_RoleId",
                table: "User",
                newName: "IX_User_RolesRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Roles_RolesRoleId",
                table: "User",
                column: "RolesRoleId",
                principalTable: "Roles",
                principalColumn: "RoleId");
        }
    }
}
