using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhDManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserIsExternal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime?[]>(
                name: "TaskDeadlines",
                table: "IndividualPlans",
                type: "timestamp with time zone[]",
                nullable: false,
                defaultValue: new DateTime?[0]);

            migrationBuilder.AddColumn<string[]>(
                name: "Tasks",
                table: "IndividualPlans",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.AddColumn<bool>(
                name: "IsExternal",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskDeadlines",
                table: "IndividualPlans");

            migrationBuilder.DropColumn(
                name: "Tasks",
                table: "IndividualPlans");

            migrationBuilder.DropColumn(
                name: "IsExternal",
                table: "AspNetUsers");
        }
    }
}
