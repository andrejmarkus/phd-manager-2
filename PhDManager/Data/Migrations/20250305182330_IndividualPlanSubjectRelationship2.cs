using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class IndividualPlanSubjectRelationship2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndividualPlanSubjectGrade_IndividualPlans_IndividualPlanId1",
                table: "IndividualPlanSubjectGrade");

            migrationBuilder.DropForeignKey(
                name: "FK_IndividualPlanSubjectGrade_Subjects_SubjectId1",
                table: "IndividualPlanSubjectGrade");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IndividualPlanSubjectGrade",
                table: "IndividualPlanSubjectGrade");

            migrationBuilder.DropIndex(
                name: "IX_IndividualPlanSubjectGrade_IndividualPlanId1",
                table: "IndividualPlanSubjectGrade");

            migrationBuilder.RenameColumn(
                name: "SubjectId1",
                table: "IndividualPlanSubjectGrade",
                newName: "SubjectsId");

            migrationBuilder.RenameColumn(
                name: "IndividualPlanId1",
                table: "IndividualPlanSubjectGrade",
                newName: "IndividualPlansId");

            migrationBuilder.RenameIndex(
                name: "IX_IndividualPlanSubjectGrade_SubjectId1",
                table: "IndividualPlanSubjectGrade",
                newName: "IX_IndividualPlanSubjectGrade_SubjectsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndividualPlanSubjectGrade",
                table: "IndividualPlanSubjectGrade",
                columns: new[] { "IndividualPlansId", "SubjectsId" });

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlanSubjectGrade_IndividualPlanId",
                table: "IndividualPlanSubjectGrade",
                column: "IndividualPlanId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "SubjectsId",
                table: "IndividualPlanSubjectGrade",
                newName: "SubjectId1");

            migrationBuilder.RenameColumn(
                name: "IndividualPlansId",
                table: "IndividualPlanSubjectGrade",
                newName: "IndividualPlanId1");

            migrationBuilder.RenameIndex(
                name: "IX_IndividualPlanSubjectGrade_SubjectsId",
                table: "IndividualPlanSubjectGrade",
                newName: "IX_IndividualPlanSubjectGrade_SubjectId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndividualPlanSubjectGrade",
                table: "IndividualPlanSubjectGrade",
                columns: new[] { "IndividualPlanId", "SubjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlanSubjectGrade_IndividualPlanId1",
                table: "IndividualPlanSubjectGrade",
                column: "IndividualPlanId1");

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualPlanSubjectGrade_IndividualPlans_IndividualPlanId1",
                table: "IndividualPlanSubjectGrade",
                column: "IndividualPlanId1",
                principalTable: "IndividualPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualPlanSubjectGrade_Subjects_SubjectId1",
                table: "IndividualPlanSubjectGrade",
                column: "SubjectId1",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
