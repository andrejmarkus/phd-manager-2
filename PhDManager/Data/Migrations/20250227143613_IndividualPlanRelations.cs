using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class IndividualPlanRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRequired",
                table: "Subjects",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "WrittenThesisRecommendedLectures",
                table: "IndividualPlans",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WrittenThesisRecommendedStudyLiterature",
                table: "IndividualPlans",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WrittenThesisRequiredStudyLiterature",
                table: "IndividualPlans",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WrittenThesisTitle",
                table: "IndividualPlans",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "IndividualPlanSubject1",
                columns: table => new
                {
                    OptionalIndividualPlansId = table.Column<int>(type: "integer", nullable: false),
                    OptionalSubjectsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualPlanSubject1", x => new { x.OptionalIndividualPlansId, x.OptionalSubjectsId });
                    table.ForeignKey(
                        name: "FK_IndividualPlanSubject1_IndividualPlans_OptionalIndividualPl~",
                        column: x => x.OptionalIndividualPlansId,
                        principalTable: "IndividualPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndividualPlanSubject1_Subjects_OptionalSubjectsId",
                        column: x => x.OptionalSubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 6,
                column: "IsRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 7,
                column: "IsRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 8,
                column: "IsRequired",
                value: true);

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlanSubject1_OptionalSubjectsId",
                table: "IndividualPlanSubject1",
                column: "OptionalSubjectsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndividualPlanSubject1");

            migrationBuilder.DropColumn(
                name: "IsRequired",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "WrittenThesisRecommendedLectures",
                table: "IndividualPlans");

            migrationBuilder.DropColumn(
                name: "WrittenThesisRecommendedStudyLiterature",
                table: "IndividualPlans");

            migrationBuilder.DropColumn(
                name: "WrittenThesisRequiredStudyLiterature",
                table: "IndividualPlans");

            migrationBuilder.DropColumn(
                name: "WrittenThesisTitle",
                table: "IndividualPlans");
        }
    }
}
