using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedThesis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectThesis");

            migrationBuilder.DropColumn(
                name: "FieldName",
                table: "StudyPrograms");

            migrationBuilder.AddColumn<string>(
                name: "StudyFieldName",
                table: "Theses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<List<string>>(
                name: "SubjectNames",
                table: "Theses",
                type: "text[]",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudyFieldName",
                table: "Theses");

            migrationBuilder.DropColumn(
                name: "SubjectNames",
                table: "Theses");

            migrationBuilder.AddColumn<string>(
                name: "FieldName",
                table: "StudyPrograms",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SubjectThesis",
                columns: table => new
                {
                    SubjectsId = table.Column<int>(type: "integer", nullable: false),
                    ThesesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectThesis", x => new { x.SubjectsId, x.ThesesId });
                    table.ForeignKey(
                        name: "FK_SubjectThesis_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectThesis_Theses_ThesesId",
                        column: x => x.ThesesId,
                        principalTable: "Theses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_SubjectThesis_ThesesId",
                table: "SubjectThesis",
                column: "ThesesId");
        }
    }
}
