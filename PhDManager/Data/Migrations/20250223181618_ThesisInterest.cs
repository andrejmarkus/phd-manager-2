using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class ThesisInterest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentThesisInterestId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StudentThesisInterestId",
                table: "AspNetUsers",
                column: "StudentThesisInterestId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Theses_StudentThesisInterestId",
                table: "AspNetUsers",
                column: "StudentThesisInterestId",
                principalTable: "Theses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Theses_StudentThesisInterestId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StudentThesisInterestId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StudentThesisInterestId",
                table: "AspNetUsers");
        }
    }
}
