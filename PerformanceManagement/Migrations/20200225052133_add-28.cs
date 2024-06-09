using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add28 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BehaviouralCompetency_PeriodDefinitoion_PeriodDefinitoionId",
                table: "BehaviouralCompetency");

            migrationBuilder.DropIndex(
                name: "IX_BehaviouralCompetency_PeriodDefinitoionId",
                table: "BehaviouralCompetency");

            migrationBuilder.DropColumn(
                name: "PeriodDefinitoionId",
                table: "BehaviouralCompetency");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeriodDefinitoionId",
                table: "BehaviouralCompetency",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BehaviouralCompetency_PeriodDefinitoionId",
                table: "BehaviouralCompetency",
                column: "PeriodDefinitoionId");

            migrationBuilder.AddForeignKey(
                name: "FK_BehaviouralCompetency_PeriodDefinitoion_PeriodDefinitoionId",
                table: "BehaviouralCompetency",
                column: "PeriodDefinitoionId",
                principalTable: "PeriodDefinitoion",
                principalColumn: "PeriodDefinitoionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
