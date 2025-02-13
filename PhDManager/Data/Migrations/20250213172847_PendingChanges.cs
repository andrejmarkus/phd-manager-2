using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class PendingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_IndividualPlan_IndividualPlanId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_ReviewerId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Theses_ThesisId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_IndividualPlan_Theses_Id",
                table: "IndividualPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_IndividualPlanSubject_IndividualPlan_IndividualPlansId",
                table: "IndividualPlanSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IndividualPlan",
                table: "IndividualPlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "IndividualPlan",
                newName: "IndividualPlans");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ThesisId",
                table: "Comments",
                newName: "IX_Comments_ThesisId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ReviewerId",
                table: "Comments",
                newName: "IX_Comments_ReviewerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndividualPlans",
                table: "IndividualPlans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_IndividualPlans_IndividualPlanId",
                table: "AspNetUsers",
                column: "IndividualPlanId",
                principalTable: "IndividualPlans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_ReviewerId",
                table: "Comments",
                column: "ReviewerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Theses_ThesisId",
                table: "Comments",
                column: "ThesisId",
                principalTable: "Theses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualPlans_Theses_Id",
                table: "IndividualPlans",
                column: "Id",
                principalTable: "Theses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualPlanSubject_IndividualPlans_IndividualPlansId",
                table: "IndividualPlanSubject",
                column: "IndividualPlansId",
                principalTable: "IndividualPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_IndividualPlans_IndividualPlanId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_ReviewerId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Theses_ThesisId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_IndividualPlans_Theses_Id",
                table: "IndividualPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_IndividualPlanSubject_IndividualPlans_IndividualPlansId",
                table: "IndividualPlanSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IndividualPlans",
                table: "IndividualPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "IndividualPlans",
                newName: "IndividualPlan");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ThesisId",
                table: "Comment",
                newName: "IX_Comment_ThesisId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ReviewerId",
                table: "Comment",
                newName: "IX_Comment_ReviewerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndividualPlan",
                table: "IndividualPlan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_IndividualPlan_IndividualPlanId",
                table: "AspNetUsers",
                column: "IndividualPlanId",
                principalTable: "IndividualPlan",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_ReviewerId",
                table: "Comment",
                column: "ReviewerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Theses_ThesisId",
                table: "Comment",
                column: "ThesisId",
                principalTable: "Theses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualPlan_Theses_Id",
                table: "IndividualPlan",
                column: "Id",
                principalTable: "Theses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualPlanSubject_IndividualPlan_IndividualPlansId",
                table: "IndividualPlanSubject",
                column: "IndividualPlansId",
                principalTable: "IndividualPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
