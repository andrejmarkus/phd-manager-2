using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class StudentStateEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Students",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Students");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Students",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
