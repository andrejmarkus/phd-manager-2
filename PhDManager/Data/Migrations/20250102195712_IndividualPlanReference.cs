using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class IndividualPlanReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndividualPlan_AspNetUsers_UserId",
                table: "IndividualPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_IndividualPlan_Theses_ThesisId",
                table: "IndividualPlan");

            migrationBuilder.DropIndex(
                name: "IX_IndividualPlan_ThesisId",
                table: "IndividualPlan");

            migrationBuilder.DropIndex(
                name: "IX_IndividualPlan_UserId",
                table: "IndividualPlan");

            migrationBuilder.DropColumn(
                name: "ThesisId",
                table: "IndividualPlan");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "IndividualPlan");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Theses",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "IndividualPlanId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IndividualPlanId",
                table: "AspNetUsers",
                column: "IndividualPlanId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_IndividualPlan_IndividualPlanId",
                table: "AspNetUsers",
                column: "IndividualPlanId",
                principalTable: "IndividualPlan",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Theses_IndividualPlan_Id",
                table: "Theses",
                column: "Id",
                principalTable: "IndividualPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_IndividualPlan_IndividualPlanId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Theses_IndividualPlan_Id",
                table: "Theses");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IndividualPlanId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IndividualPlanId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Theses",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "ThesisId",
                table: "IndividualPlan",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "IndividualPlan",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlan_ThesisId",
                table: "IndividualPlan",
                column: "ThesisId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlan_UserId",
                table: "IndividualPlan",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualPlan_AspNetUsers_UserId",
                table: "IndividualPlan",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualPlan_Theses_ThesisId",
                table: "IndividualPlan",
                column: "ThesisId",
                principalTable: "Theses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
