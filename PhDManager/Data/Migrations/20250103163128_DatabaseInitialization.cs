using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseInitialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "StudyPrograms",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "AI", "Aplikovaná informatika" },
                    { 2, "MAN", "Manažment" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Code", "Credits", "Name", "Semester", "StudyProgramId" },
                values: new object[,]
                {
                    { 1, "MPI", 5, "Matematické peincípy informatiky", "zimný", 1 },
                    { 2, "TM AI", 5, "Teória a metodológia aplikovanej informatiky", "zimný", 1 },
                    { 3, "JAD1", 5, "Jazyk anglický PhD. 1", "zimný", 1 },
                    { 4, "DPR1", 5, "Dizertačný projekt 1", "zimný", 1 },
                    { 5, "VPub1", 10, "Vedecká a publikačná činnosť 1", "letný", 1 },
                    { 6, "JAD2", 5, "Jazyk anglický PhD. 2", "letný", 1 },
                    { 7, "DPR2", 5, "Dizertačný projekt 2", "letný", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StudyPrograms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "StudyPrograms",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
