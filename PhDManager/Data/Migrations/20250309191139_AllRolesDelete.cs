using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class AllRolesDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_AspNetUsers_UserId",
                table: "Admin");

            migrationBuilder.DropForeignKey(
                name: "FK_External_AspNetUsers_UserId",
                table: "External");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_AspNetUsers_UserId",
                table: "Admin",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_External_AspNetUsers_UserId",
                table: "External",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_AspNetUsers_UserId",
                table: "Admin");

            migrationBuilder.DropForeignKey(
                name: "FK_External_AspNetUsers_UserId",
                table: "External");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_AspNetUsers_UserId",
                table: "Admin",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_External_AspNetUsers_UserId",
                table: "External",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
