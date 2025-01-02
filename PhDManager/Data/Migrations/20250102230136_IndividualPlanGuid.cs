using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class IndividualPlanGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Guid",
                table: "IndividualPlan",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                table: "IndividualPlan");
        }
    }
}
