using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class IndividualPlanSubjectRelationship3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndividualPlanSubjectGrade_IndividualPlans_IndividualPlansId",
                table: "IndividualPlanSubjectGrade");

            migrationBuilder.DropForeignKey(
                name: "FK_IndividualPlanSubjectGrade_Subjects_SubjectsId",
                table: "IndividualPlanSubjectGrade");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IndividualPlanSubjectGrade",
                table: "IndividualPlanSubjectGrade");

            migrationBuilder.DropIndex(
                name: "IX_IndividualPlanSubjectGrade_IndividualPlanId",
                table: "IndividualPlanSubjectGrade");

            migrationBuilder.DropIndex(
                name: "IX_IndividualPlanSubjectGrade_SubjectsId",
                table: "IndividualPlanSubjectGrade");

            migrationBuilder.DropColumn(
                name: "IndividualPlansId",
                table: "IndividualPlanSubjectGrade");

            migrationBuilder.DropColumn(
                name: "SubjectsId",
                table: "IndividualPlanSubjectGrade");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndividualPlanSubjectGrade",
                table: "IndividualPlanSubjectGrade",
                columns: new[] { "IndividualPlanId", "SubjectId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IndividualPlanSubjectGrade",
                table: "IndividualPlanSubjectGrade");

            migrationBuilder.AddColumn<int>(
                name: "IndividualPlansId",
                table: "IndividualPlanSubjectGrade",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubjectsId",
                table: "IndividualPlanSubjectGrade",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndividualPlanSubjectGrade",
                table: "IndividualPlanSubjectGrade",
                columns: new[] { "IndividualPlansId", "SubjectsId" });

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlanSubjectGrade_IndividualPlanId",
                table: "IndividualPlanSubjectGrade",
                column: "IndividualPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlanSubjectGrade_SubjectsId",
                table: "IndividualPlanSubjectGrade",
                column: "SubjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualPlanSubjectGrade_IndividualPlans_IndividualPlansId",
                table: "IndividualPlanSubjectGrade",
                column: "IndividualPlansId",
                principalTable: "IndividualPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualPlanSubjectGrade_Subjects_SubjectsId",
                table: "IndividualPlanSubjectGrade",
                column: "SubjectsId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
