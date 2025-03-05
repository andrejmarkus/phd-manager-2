using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class IndividualPlanSubjectRelationship4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndividualPlanSubjectGrade");

            migrationBuilder.CreateTable(
                name: "IndividualPlanSubject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IndividualPlanId = table.Column<int>(type: "integer", nullable: false),
                    SubjectId = table.Column<int>(type: "integer", nullable: false),
                    Grade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualPlanSubject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualPlanSubject_IndividualPlans_IndividualPlanId",
                        column: x => x.IndividualPlanId,
                        principalTable: "IndividualPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndividualPlanSubject_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlanSubject_IndividualPlanId",
                table: "IndividualPlanSubject",
                column: "IndividualPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlanSubject_SubjectId",
                table: "IndividualPlanSubject",
                column: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndividualPlanSubject");

            migrationBuilder.CreateTable(
                name: "IndividualPlanSubjectGrade",
                columns: table => new
                {
                    IndividualPlanId = table.Column<int>(type: "integer", nullable: false),
                    SubjectId = table.Column<int>(type: "integer", nullable: false),
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
                        name: "FK_IndividualPlanSubjectGrade_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlanSubjectGrade_SubjectId",
                table: "IndividualPlanSubjectGrade",
                column: "SubjectId");
        }
    }
}
