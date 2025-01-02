using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class IndividualPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IndividualPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DissertationExamDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DissertationSubmissionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StudyEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    ThesisId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualPlan_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IndividualPlan_Theses_ThesisId",
                        column: x => x.ThesisId,
                        principalTable: "Theses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlan_ThesisId",
                table: "IndividualPlan",
                column: "ThesisId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlan_UserId",
                table: "IndividualPlan",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndividualPlan");
        }
    }
}
