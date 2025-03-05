using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class IndividualPlanSubjectRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndividualPlanSubject");

            migrationBuilder.DropTable(
                name: "IndividualPlanSubject1");

            migrationBuilder.CreateTable(
                name: "IndividualPlanSubjectGrade",
                columns: table => new
                {
                    IndividualPlanId = table.Column<int>(type: "integer", nullable: false),
                    SubjectId = table.Column<int>(type: "integer", nullable: false),
                    IndividualPlanId1 = table.Column<int>(type: "integer", nullable: false),
                    SubjectId1 = table.Column<int>(type: "integer", nullable: false),
                    Grade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualPlanSubjectGrade", x => new { x.IndividualPlanId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_IndividualPlanSubjectGrade_IndividualPlans_IndividualPlanId",
                        column: x => x.IndividualPlanId,
                        principalTable: "IndividualPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndividualPlanSubjectGrade_IndividualPlans_IndividualPlanId1",
                        column: x => x.IndividualPlanId1,
                        principalTable: "IndividualPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndividualPlanSubjectGrade_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndividualPlanSubjectGrade_Subjects_SubjectId1",
                        column: x => x.SubjectId1,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlanSubjectGrade_IndividualPlanId1",
                table: "IndividualPlanSubjectGrade",
                column: "IndividualPlanId1");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlanSubjectGrade_SubjectId",
                table: "IndividualPlanSubjectGrade",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlanSubjectGrade_SubjectId1",
                table: "IndividualPlanSubjectGrade",
                column: "SubjectId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndividualPlanSubjectGrade");

            migrationBuilder.CreateTable(
                name: "IndividualPlanSubject",
                columns: table => new
                {
                    IndividualPlansId = table.Column<int>(type: "integer", nullable: false),
                    SubjectsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualPlanSubject", x => new { x.IndividualPlansId, x.SubjectsId });
                    table.ForeignKey(
                        name: "FK_IndividualPlanSubject_IndividualPlans_IndividualPlansId",
                        column: x => x.IndividualPlansId,
                        principalTable: "IndividualPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndividualPlanSubject_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlanSubject_SubjectsId",
                table: "IndividualPlanSubject",
                column: "SubjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlanSubject1_OptionalSubjectsId",
                table: "IndividualPlanSubject1",
                column: "OptionalSubjectsId");
        }
    }
}
