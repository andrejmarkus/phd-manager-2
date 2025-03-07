using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class StudentEvaluation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentEvaluation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    SchoolYear = table.Column<string>(type: "text", nullable: false),
                    ThesisState = table.Column<string>(type: "text", nullable: false),
                    CreditsEvaluation = table.Column<string>(type: "text", nullable: false),
                    ScientificEvaluation = table.Column<string>(type: "text", nullable: false),
                    AssignmentsState = table.Column<string>(type: "text", nullable: false),
                    ModificationProposal = table.Column<string>(type: "text", nullable: false),
                    AdditionalEvaluation = table.Column<string>(type: "text", nullable: false),
                    RealDissertationExamDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RealDissertationSubmissionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RealStudyEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Conclusion = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEvaluation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentEvaluation_Students_Id",
                        column: x => x.Id,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentEvaluation");
        }
    }
}
