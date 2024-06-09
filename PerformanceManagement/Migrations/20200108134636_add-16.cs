using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChartConfirmation",
                columns: table => new
                {
                    ChartConfirmationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EvaluationHierarchyId = table.Column<int>(nullable: false),
                    PeriodDefinitionId = table.Column<int>(nullable: false),
                    EvalHieEffectiveStartDate = table.Column<DateTime>(nullable: false),
                    Confirmation = table.Column<bool>(nullable: false),
                    CauseDescription = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChartConfirmation", x => new { x.ChartConfirmationId, x.EvaluationHierarchyId, x.PeriodDefinitionId });
                    table.ForeignKey(
                        name: "FK_ChartConfirmation_PeriodDefinitoion_PeriodDefinitionId",
                        column: x => x.PeriodDefinitionId,
                        principalTable: "PeriodDefinitoion",
                        principalColumn: "PeriodDefinitoionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChartConfirmation_evaluationHierarchies_EvaluationHierarchyId_EvalHieEffectiveStartDate",
                        columns: x => new { x.EvaluationHierarchyId, x.EvalHieEffectiveStartDate },
                        principalTable: "evaluationHierarchies",
                        principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChartConfirmation_PeriodDefinitionId",
                table: "ChartConfirmation",
                column: "PeriodDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ChartConfirmation_EvaluationHierarchyId_EvalHieEffectiveStartDate",
                table: "ChartConfirmation",
                columns: new[] { "EvaluationHierarchyId", "EvalHieEffectiveStartDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChartConfirmation");
        }
    }
}
