using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class FieldName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FieldName",
                table: "StudyPrograms",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "StudyPrograms",
                keyColumn: "Id",
                keyValue: 1,
                column: "FieldName",
                value: "Informatika");

            migrationBuilder.UpdateData(
                table: "StudyPrograms",
                keyColumn: "Id",
                keyValue: 2,
                column: "FieldName",
                value: "Manažment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FieldName",
                table: "StudyPrograms");
        }
    }
}
