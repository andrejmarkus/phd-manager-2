using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class TeacherRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamSupervisor_Students_StudentId",
                table: "ExamSupervisor");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamSupervisor_Teachers_AcademicCommitteeMemberId",
                table: "ExamSupervisor");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamSupervisor_Teachers_AdditionalMemberId",
                table: "ExamSupervisor");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamSupervisor_Teachers_ChairpersonId",
                table: "ExamSupervisor");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamSupervisor_Teachers_Id",
                table: "ExamSupervisor");

            migrationBuilder.DropIndex(
                name: "IX_ExamSupervisor_AcademicCommitteeMemberId",
                table: "ExamSupervisor");

            migrationBuilder.DropIndex(
                name: "IX_ExamSupervisor_AdditionalMemberId",
                table: "ExamSupervisor");

            migrationBuilder.DropIndex(
                name: "IX_ExamSupervisor_ExternalMemberId",
                table: "ExamSupervisor");

            migrationBuilder.DropIndex(
                name: "IX_ExamSupervisor_ChairpersonId",
                table: "ExamSupervisor");

            migrationBuilder.DropIndex(
                name: "IX_ExamSupervisor_StudentId",
                table: "ExamSupervisor");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "ExamSupervisor");

            migrationBuilder.AlterColumn<int>(
                name: "ChairpersonId",
                table: "ExamSupervisor",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "AdditionalMemberId",
                table: "ExamSupervisor",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AcademicCommitteeMemberId",
                table: "ExamSupervisor",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "ExaminerId",
                table: "ExamSupervisor",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamSupervisor_AcademicCommitteeMemberId",
                table: "ExamSupervisor",
                column: "AcademicCommitteeMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSupervisor_AdditionalMemberId",
                table: "ExamSupervisor",
                column: "AdditionalMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSupervisor_ExaminerId",
                table: "ExamSupervisor",
                column: "ExaminerId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSupervisor_ExternalMemberId",
                table: "ExamSupervisor",
                column: "ExternalMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSupervisor_ChairpersonId",
                table: "ExamSupervisor",
                column: "ChairpersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSupervisor_Students_Id",
                table: "ExamSupervisor",
                column: "Id",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSupervisor_Teachers_AcademicCommitteeMemberId",
                table: "ExamSupervisor",
                column: "AcademicCommitteeMemberId",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSupervisor_Teachers_AdditionalMemberId",
                table: "ExamSupervisor",
                column: "AdditionalMemberId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSupervisor_Teachers_ChairpersonId",
                table: "ExamSupervisor",
                column: "ChairpersonId",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSupervisor_Teachers_ExaminerId",
                table: "ExamSupervisor",
                column: "ExaminerId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamSupervisor_Students_Id",
                table: "ExamSupervisor");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamSupervisor_Teachers_AcademicCommitteeMemberId",
                table: "ExamSupervisor");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamSupervisor_Teachers_AdditionalMemberId",
                table: "ExamSupervisor");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamSupervisor_Teachers_ChairpersonId",
                table: "ExamSupervisor");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamSupervisor_Teachers_ExaminerId",
                table: "ExamSupervisor");

            migrationBuilder.DropIndex(
                name: "IX_ExamSupervisor_AcademicCommitteeMemberId",
                table: "ExamSupervisor");

            migrationBuilder.DropIndex(
                name: "IX_ExamSupervisor_AdditionalMemberId",
                table: "ExamSupervisor");

            migrationBuilder.DropIndex(
                name: "IX_ExamSupervisor_ExaminerId",
                table: "ExamSupervisor");

            migrationBuilder.DropIndex(
                name: "IX_ExamSupervisor_ExternalMemberId",
                table: "ExamSupervisor");

            migrationBuilder.DropIndex(
                name: "IX_ExamSupervisor_ChairpersonId",
                table: "ExamSupervisor");

            migrationBuilder.DropColumn(
                name: "ExaminerId",
                table: "ExamSupervisor");

            migrationBuilder.AlterColumn<int>(
                name: "ChairpersonId",
                table: "ExamSupervisor",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AdditionalMemberId",
                table: "ExamSupervisor",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "AcademicCommitteeMemberId",
                table: "ExamSupervisor",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "ExamSupervisor",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExamSupervisor_AcademicCommitteeMemberId",
                table: "ExamSupervisor",
                column: "AcademicCommitteeMemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamSupervisor_AdditionalMemberId",
                table: "ExamSupervisor",
                column: "AdditionalMemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamSupervisor_ExternalMemberId",
                table: "ExamSupervisor",
                column: "ExternalMemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamSupervisor_ChairpersonId",
                table: "ExamSupervisor",
                column: "ChairpersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamSupervisor_StudentId",
                table: "ExamSupervisor",
                column: "StudentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSupervisor_Students_StudentId",
                table: "ExamSupervisor",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSupervisor_Teachers_AcademicCommitteeMemberId",
                table: "ExamSupervisor",
                column: "AcademicCommitteeMemberId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSupervisor_Teachers_AdditionalMemberId",
                table: "ExamSupervisor",
                column: "AdditionalMemberId",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSupervisor_Teachers_ChairpersonId",
                table: "ExamSupervisor",
                column: "ChairpersonId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSupervisor_Teachers_Id",
                table: "ExamSupervisor",
                column: "Id",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
