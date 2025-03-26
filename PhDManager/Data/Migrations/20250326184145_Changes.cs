using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations");

            migrationBuilder.RenameTable(
                name: "Registrations",
                newName: "Invitations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invitations",
                table: "Invitations",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Invitations",
                table: "Invitations");

            migrationBuilder.RenameTable(
                name: "Invitations",
                newName: "Registrations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations",
                column: "Id");
        }
    }
}
