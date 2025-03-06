using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExamApplications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExternal",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Students",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Students",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExamApplicationId",
                table: "Students",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudyForm",
                table: "Students",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ExamApplication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WrittenThesisTitle = table.Column<string>(type: "text", nullable: false),
                    WrittenThesisTitleEnglish = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamApplication", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectsExamApplication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectsExamApplication", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamApplicationSubject",
                columns: table => new
                {
                    ExamApplicationsId = table.Column<int>(type: "integer", nullable: false),
                    SubjectsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamApplicationSubject", x => new { x.ExamApplicationsId, x.SubjectsId });
                    table.ForeignKey(
                        name: "FK_ExamApplicationSubject_ExamApplication_ExamApplicationsId",
                        column: x => x.ExamApplicationsId,
                        principalTable: "ExamApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamApplicationSubject_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectSubjectsExamApplication",
                columns: table => new
                {
                    SubjectsExamApplicationsId = table.Column<int>(type: "integer", nullable: false),
                    SubjectsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectSubjectsExamApplication", x => new { x.SubjectsExamApplicationsId, x.SubjectsId });
                    table.ForeignKey(
                        name: "FK_SubjectSubjectsExamApplication_SubjectsExamApplication_Subj~",
                        column: x => x.SubjectsExamApplicationsId,
                        principalTable: "SubjectsExamApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectSubjectsExamApplication_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_DepartmentId",
                table: "Students",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ExamApplicationId",
                table: "Students",
                column: "ExamApplicationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamApplicationSubject_SubjectsId",
                table: "ExamApplicationSubject",
                column: "SubjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectSubjectsExamApplication_SubjectsId",
                table: "SubjectSubjectsExamApplication",
                column: "SubjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DepartmentId",
                table: "Students",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_ExamApplication_ExamApplicationId",
                table: "Students",
                column: "ExamApplicationId",
                principalTable: "ExamApplication",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_SubjectsExamApplication_Id",
                table: "Students",
                column: "Id",
                principalTable: "SubjectsExamApplication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DepartmentId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_ExamApplication_ExamApplicationId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_SubjectsExamApplication_Id",
                table: "Students");

            migrationBuilder.DropTable(
                name: "ExamApplicationSubject");

            migrationBuilder.DropTable(
                name: "SubjectSubjectsExamApplication");

            migrationBuilder.DropTable(
                name: "ExamApplication");

            migrationBuilder.DropTable(
                name: "SubjectsExamApplication");

            migrationBuilder.DropIndex(
                name: "IX_Students_DepartmentId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ExamApplicationId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ExamApplicationId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudyForm",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Students",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<bool>(
                name: "IsExternal",
                table: "Students",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
