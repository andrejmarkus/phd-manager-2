using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExamApplicationsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_ExamApplication_ExamApplicationId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_SubjectsExamApplication_Id",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ExamApplicationId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ExamApplicationId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "SubjectsExamApplication",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Students",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ExamApplication",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamApplication_Students_Id",
                table: "ExamApplication",
                column: "Id",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectsExamApplication_Students_Id",
                table: "SubjectsExamApplication",
                column: "Id",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamApplication_Students_Id",
                table: "ExamApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectsExamApplication_Students_Id",
                table: "SubjectsExamApplication");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "SubjectsExamApplication",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Students",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "ExamApplicationId",
                table: "Students",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ExamApplication",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Students_ExamApplicationId",
                table: "Students",
                column: "ExamApplicationId",
                unique: true);

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
    }
}
