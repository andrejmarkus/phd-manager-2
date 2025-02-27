using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class IndividualPlanModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WrittenThesisRequiredStudyLiterature",
                table: "IndividualPlans",
                newName: "WrittenThesisRequiredLiterature");

            migrationBuilder.RenameColumn(
                name: "WrittenThesisRecommendedStudyLiterature",
                table: "IndividualPlans",
                newName: "WrittenThesisRecommendedLiterature");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WrittenThesisRequiredLiterature",
                table: "IndividualPlans",
                newName: "WrittenThesisRequiredStudyLiterature");

            migrationBuilder.RenameColumn(
                name: "WrittenThesisRecommendedLiterature",
                table: "IndividualPlans",
                newName: "WrittenThesisRecommendedStudyLiterature");
        }
    }
}
