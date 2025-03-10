using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class StudentChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_External_ExternalId1",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "External");

            migrationBuilder.DropColumn(
                name: "IsRequired",
                table: "Subjects");

            migrationBuilder.RenameColumn(
                name: "RealStudyEndDate",
                table: "StudentEvaluation",
                newName: "PlannedDissertationSubmissionDate");

            migrationBuilder.RenameColumn(
                name: "RealDissertationSubmissionDate",
                table: "StudentEvaluation",
                newName: "PlannedDissertationExamDate");

            migrationBuilder.RenameColumn(
                name: "RealDissertationExamDate",
                table: "StudentEvaluation",
                newName: "PlannedDissertationApplicationDate");

            migrationBuilder.RenameColumn(
                name: "DissertationExamDate",
                table: "IndividualPlans",
                newName: "DissertationApplicationDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "CurrentDate",
                table: "SubjectsExamApplication",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RequirementLevel",
                table: "Subjects",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DissertationExamDate",
                table: "Students",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DissertationExamResult",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DissertationExamTranscript",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CurrentDate",
                table: "IndividualPlans",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CurrentDate",
                table: "ExamApplication",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ExternalTeacher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalTeacher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalTeacher_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequirementLevel",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 2,
                column: "RequirementLevel",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 3,
                column: "RequirementLevel",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 4,
                column: "RequirementLevel",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 5,
                column: "RequirementLevel",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 6,
                column: "RequirementLevel",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 7,
                column: "RequirementLevel",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 8,
                column: "RequirementLevel",
                value: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExternalTeacher_UserId",
                table: "ExternalTeacher",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ExternalTeacher_ExternalId1",
                table: "Comments",
                column: "ExternalId1",
                principalTable: "ExternalTeacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ExternalTeacher_ExternalId1",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "ExternalTeacher");

            migrationBuilder.DropColumn(
                name: "CurrentDate",
                table: "SubjectsExamApplication");

            migrationBuilder.DropColumn(
                name: "RequirementLevel",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "DissertationExamDate",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DissertationExamResult",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DissertationExamTranscript",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CurrentDate",
                table: "IndividualPlans");

            migrationBuilder.DropColumn(
                name: "CurrentDate",
                table: "ExamApplication");

            migrationBuilder.RenameColumn(
                name: "PlannedDissertationSubmissionDate",
                table: "StudentEvaluation",
                newName: "RealStudyEndDate");

            migrationBuilder.RenameColumn(
                name: "PlannedDissertationExamDate",
                table: "StudentEvaluation",
                newName: "RealDissertationSubmissionDate");

            migrationBuilder.RenameColumn(
                name: "PlannedDissertationApplicationDate",
                table: "StudentEvaluation",
                newName: "RealDissertationExamDate");

            migrationBuilder.RenameColumn(
                name: "DissertationApplicationDate",
                table: "IndividualPlans",
                newName: "DissertationExamDate");

            migrationBuilder.AddColumn<bool>(
                name: "IsRequired",
                table: "Subjects",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "External",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_External", x => x.Id);
                    table.ForeignKey(
                        name: "FK_External_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
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
                name: "IX_External_UserId",
                table: "External",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_External_ExternalId1",
                table: "Comments",
                column: "ExternalId1",
                principalTable: "External",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
