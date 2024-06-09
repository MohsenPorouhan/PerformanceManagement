using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EvaluationCoefficientId",
                table: "PeriodDefinitoion",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EvaluationCoefficient",
                columns: table => new
                {
                    EvaluationCoefficientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    level1CoacherTWith = table.Column<int>(nullable: false),
                    level2CoacherTWith = table.Column<int>(nullable: false),
                    selfEvaluationTWith = table.Column<int>(nullable: false),
                    participantCoefficientT = table.Column<int>(nullable: false),
                    level1CoacherTWithout = table.Column<int>(nullable: false),
                    level2CoacherTWithout = table.Column<int>(nullable: false),
                    selfEvaluationTWithout = table.Column<int>(nullable: false),
                    level1CoacherBWith = table.Column<int>(nullable: false),
                    level2CoacherBWith = table.Column<int>(nullable: false),
                    selfEvaluationBWith = table.Column<int>(nullable: false),
                    participantCoefficientB = table.Column<int>(nullable: false),
                    level1CoacherBWithout = table.Column<int>(nullable: false),
                    level2CoacherBWithout = table.Column<int>(nullable: false),
                    selfEvaluationBWithout = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationCoefficient", x => x.EvaluationCoefficientId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeriodDefinitoion_EvaluationCoefficientId",
                table: "PeriodDefinitoion",
                column: "EvaluationCoefficientId",
                unique: true,
                filter: "[EvaluationCoefficientId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PeriodDefinitoion_EvaluationCoefficient_EvaluationCoefficientId",
                table: "PeriodDefinitoion",
                column: "EvaluationCoefficientId",
                principalTable: "EvaluationCoefficient",
                principalColumn: "EvaluationCoefficientId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeriodDefinitoion_EvaluationCoefficient_EvaluationCoefficientId",
                table: "PeriodDefinitoion");

            migrationBuilder.DropTable(
                name: "EvaluationCoefficient");

            migrationBuilder.DropIndex(
                name: "IX_PeriodDefinitoion_EvaluationCoefficientId",
                table: "PeriodDefinitoion");

            migrationBuilder.DropColumn(
                name: "EvaluationCoefficientId",
                table: "PeriodDefinitoion");
        }
    }
}
