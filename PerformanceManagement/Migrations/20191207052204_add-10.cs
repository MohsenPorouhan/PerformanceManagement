using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PeriodTitle",
                table: "PeriodDefinitoion",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PeriodDefinitoion_PeriodCode",
                table: "PeriodDefinitoion",
                column: "PeriodCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PeriodDefinitoion_PeriodTitle",
                table: "PeriodDefinitoion",
                column: "PeriodTitle",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PeriodDefinitoion_PeriodCode",
                table: "PeriodDefinitoion");

            migrationBuilder.DropIndex(
                name: "IX_PeriodDefinitoion_PeriodTitle",
                table: "PeriodDefinitoion");

            migrationBuilder.AlterColumn<string>(
                name: "PeriodTitle",
                table: "PeriodDefinitoion",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
