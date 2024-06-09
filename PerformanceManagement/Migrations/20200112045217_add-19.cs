using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SupervisorEffectiveStartDate",
                table: "ChartConfirmation",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupervisorId",
                table: "ChartConfirmation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ChartConfirmation_SupervisorId_SupervisorEffectiveStartDate",
                table: "ChartConfirmation",
                columns: new[] { "SupervisorId", "SupervisorEffectiveStartDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_ChartConfirmation_People_SupervisorId_SupervisorEffectiveStartDate",
                table: "ChartConfirmation",
                columns: new[] { "SupervisorId", "SupervisorEffectiveStartDate" },
                principalTable: "People",
                principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChartConfirmation_People_SupervisorId_SupervisorEffectiveStartDate",
                table: "ChartConfirmation");

            migrationBuilder.DropIndex(
                name: "IX_ChartConfirmation_SupervisorId_SupervisorEffectiveStartDate",
                table: "ChartConfirmation");

            migrationBuilder.DropColumn(
                name: "SupervisorEffectiveStartDate",
                table: "ChartConfirmation");

            migrationBuilder.DropColumn(
                name: "SupervisorId",
                table: "ChartConfirmation");
        }
    }
}
