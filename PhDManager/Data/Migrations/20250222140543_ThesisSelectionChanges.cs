using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class ThesisSelectionChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_IndividualPlans_IndividualPlanId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_IndividualPlans_Theses_Id",
                table: "IndividualPlans");

            migrationBuilder.RenameColumn(
                name: "IndividualPlanId",
                table: "AspNetUsers",
                newName: "StudentIndividualPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_IndividualPlanId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_StudentIndividualPlanId");

            migrationBuilder.AlterColumn<List<string>>(
                name: "SubjectNames",
                table: "Theses",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(List<string>),
                oldType: "text[]");

            migrationBuilder.AddColumn<List<string>>(
                name: "SchoolYears",
                table: "Theses",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Theses",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "IndividualPlans",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Theses_StudentId",
                table: "Theses",
                column: "StudentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_IndividualPlans_StudentIndividualPlanId",
                table: "AspNetUsers",
                column: "StudentIndividualPlanId",
                principalTable: "IndividualPlans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Theses_AspNetUsers_StudentId",
                table: "Theses",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_IndividualPlans_StudentIndividualPlanId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Theses_AspNetUsers_StudentId",
                table: "Theses");

            migrationBuilder.DropIndex(
                name: "IX_Theses_StudentId",
                table: "Theses");

            migrationBuilder.DropColumn(
                name: "SchoolYears",
                table: "Theses");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Theses");

            migrationBuilder.RenameColumn(
                name: "StudentIndividualPlanId",
                table: "AspNetUsers",
                newName: "IndividualPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_StudentIndividualPlanId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_IndividualPlanId");

            migrationBuilder.AlterColumn<List<string>>(
                name: "SubjectNames",
                table: "Theses",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "IndividualPlans",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_IndividualPlans_IndividualPlanId",
                table: "AspNetUsers",
                column: "IndividualPlanId",
                principalTable: "IndividualPlans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualPlans_Theses_Id",
                table: "IndividualPlans",
                column: "Id",
                principalTable: "Theses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
