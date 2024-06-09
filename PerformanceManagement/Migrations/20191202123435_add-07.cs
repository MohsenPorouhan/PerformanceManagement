using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add07 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonPeriodEvaluationHierarchy",
                columns: table => new
                {
                    PersonPeriodEvaluationHierarchyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PeopleId = table.Column<int>(nullable: false),
                    PeopleEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    PeriodDefinitoionId = table.Column<int>(nullable: false),
                    EvaluationHierarchyId = table.Column<int>(nullable: false),
                    EvaluationHierarchyEffectiveStartDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPeriodEvaluationHierarchy", x => x.PersonPeriodEvaluationHierarchyId);
                    table.ForeignKey(
                        name: "FK_PersonPeriodEvaluationHierarchy_PeriodDefinitoion_PeriodDefinitoionId",
                        column: x => x.PeriodDefinitoionId,
                        principalTable: "PeriodDefinitoion",
                        principalColumn: "PeriodDefinitoionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonPeriodEvaluationHierarchy_evaluationHierarchies_EvaluationHierarchyId_EvaluationHierarchyEffectiveStartDate",
                        columns: x => new { x.EvaluationHierarchyId, x.EvaluationHierarchyEffectiveStartDate },
                        principalTable: "evaluationHierarchies",
                        principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonPeriodEvaluationHierarchy_People_PeopleId_PeopleEffectiveStartDate",
                        columns: x => new { x.PeopleId, x.PeopleEffectiveStartDate },
                        principalTable: "People",
                        principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonPeriodEvaluationHierarchy_PeriodDefinitoionId",
                table: "PersonPeriodEvaluationHierarchy",
                column: "PeriodDefinitoionId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonPeriodEvaluationHierarchy_EvaluationHierarchyId_EvaluationHierarchyEffectiveStartDate",
                table: "PersonPeriodEvaluationHierarchy",
                columns: new[] { "EvaluationHierarchyId", "EvaluationHierarchyEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_PersonPeriodEvaluationHierarchy_PeopleId_PeopleEffectiveStartDate",
                table: "PersonPeriodEvaluationHierarchy",
                columns: new[] { "PeopleId", "PeopleEffectiveStartDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonPeriodEvaluationHierarchy");
        }
    }
}
