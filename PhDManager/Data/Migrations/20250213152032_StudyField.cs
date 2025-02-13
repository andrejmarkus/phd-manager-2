using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class StudyField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudyFieldName",
                table: "Theses");

            migrationBuilder.AddColumn<string>(
                name: "StudyFieldName",
                table: "StudyPrograms",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "StudyPrograms",
                keyColumn: "Id",
                keyValue: 1,
                column: "StudyFieldName",
                value: "Informatika");

            migrationBuilder.UpdateData(
                table: "StudyPrograms",
                keyColumn: "Id",
                keyValue: 2,
                column: "StudyFieldName",
                value: "Ekonómia a manažment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudyFieldName",
                table: "StudyPrograms");

            migrationBuilder.AddColumn<string>(
                name: "StudyFieldName",
                table: "Theses",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
