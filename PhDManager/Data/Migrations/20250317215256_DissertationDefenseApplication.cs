using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class DissertationDefenseApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Birthplace",
                table: "Addresses",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DissertationDefenseApplication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Nationality = table.Column<string>(type: "text", nullable: false),
                    Ethnicity = table.Column<string>(type: "text", nullable: false),
                    AchievedHigherEducation = table.Column<string>(type: "text", nullable: false),
                    ApplicationSubmissionYear = table.Column<int>(type: "integer", nullable: false),
                    CurrentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DissertationDefenseApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DissertationDefenseApplication_Students_Id",
                        column: x => x.Id,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DissertationDefenseApplication");

            migrationBuilder.DropColumn(
                name: "Birthplace",
                table: "Addresses");
        }
    }
}
