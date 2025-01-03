using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class ErrorFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Code", "Credits", "Name", "Semester", "StudyProgramId" },
                values: new object[,]
                {
                    { 8, "ManT", 5, "Manažérske teórie", "zimný", 2 },
                    { 9, "MVM", 5, "Metodológia výskumu v manažmente", "zimný", 2 },
                    { 10, "JAD1", 5, "Jazyk anglický PhD. 1", "zimný", 2 },
                    { 11, "DPR1", 5, "Dizertačný projekt 1", "zimný", 2 },
                    { 12, "VPub1", 10, "Vedecká a publikačná činnosť 1", "letný", 2 },
                    { 13, "JAD2", 5, "Jazyk anglický PhD. 2", "letný", 2 },
                    { 14, "DPR2", 5, "Dizertačný projekt 2", "letný", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 14);
        }
    }
}
