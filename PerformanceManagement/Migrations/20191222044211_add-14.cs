using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "CompetencyPercent",
                table: "PeriodDefinitoion",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TaskPercent",
                table: "PeriodDefinitoion",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompetencyPercent",
                table: "PeriodDefinitoion");

            migrationBuilder.DropColumn(
                name: "TaskPercent",
                table: "PeriodDefinitoion");
        }
    }
}
