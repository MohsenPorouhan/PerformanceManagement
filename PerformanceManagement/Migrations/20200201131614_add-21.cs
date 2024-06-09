using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Evaluation",
                columns: table => new
                {
                    EvaluationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TaskId = table.Column<int>(nullable: false),
                    AllocatorEvalHieEffcStartDate = table.Column<DateTime>(nullable: true),
                    AllocatorEvaluationHierarchyId = table.Column<int>(nullable: true),
                    RecieverAllocEvalHieEffcStartDate = table.Column<DateTime>(nullable: true),
                    RecieverAllocationEvaluationHierarchyId = table.Column<int>(nullable: false),
                    AllocatorPersonEffecStartDate = table.Column<DateTime>(nullable: true),
                    AllocatorPersonId = table.Column<int>(nullable: false),
                    AllocatorRoleId = table.Column<string>(nullable: true),
                    RecieverAllocPersonEffecStartDate = table.Column<DateTime>(nullable: true),
                    RecieverAllocationPersonId = table.Column<int>(nullable: false),
                    PeriodDefinitoionId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluation", x => x.EvaluationId);
                    table.ForeignKey(
                        name: "FK_Evaluation_PeriodDefinitoion_PeriodDefinitoionId",
                        column: x => x.PeriodDefinitoionId,
                        principalTable: "PeriodDefinitoion",
                        principalColumn: "PeriodDefinitoionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluation_Task_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluation_evaluationHierarchies_AllocatorEvaluationHierarchyId_AllocatorEvalHieEffcStartDate",
                        columns: x => new { x.AllocatorEvaluationHierarchyId, x.AllocatorEvalHieEffcStartDate },
                        principalTable: "evaluationHierarchies",
                        principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluation_People_AllocatorPersonId_AllocatorPersonEffecStartDate",
                        columns: x => new { x.AllocatorPersonId, x.AllocatorPersonEffecStartDate },
                        principalTable: "People",
                        principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluation_evaluationHierarchies_RecieverAllocationEvaluationHierarchyId_RecieverAllocEvalHieEffcStartDate",
                        columns: x => new { x.RecieverAllocationEvaluationHierarchyId, x.RecieverAllocEvalHieEffcStartDate },
                        principalTable: "evaluationHierarchies",
                        principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluation_People_RecieverAllocationPersonId_RecieverAllocPersonEffecStartDate",
                        columns: x => new { x.RecieverAllocationPersonId, x.RecieverAllocPersonEffecStartDate },
                        principalTable: "People",
                        principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluation_PeriodDefinitoionId",
                table: "Evaluation",
                column: "PeriodDefinitoionId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluation_TaskId",
                table: "Evaluation",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluation_AllocatorEvaluationHierarchyId_AllocatorEvalHieEffcStartDate",
                table: "Evaluation",
                columns: new[] { "AllocatorEvaluationHierarchyId", "AllocatorEvalHieEffcStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluation_AllocatorPersonId_AllocatorPersonEffecStartDate",
                table: "Evaluation",
                columns: new[] { "AllocatorPersonId", "AllocatorPersonEffecStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluation_RecieverAllocationEvaluationHierarchyId_RecieverAllocEvalHieEffcStartDate",
                table: "Evaluation",
                columns: new[] { "RecieverAllocationEvaluationHierarchyId", "RecieverAllocEvalHieEffcStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluation_RecieverAllocationPersonId_RecieverAllocPersonEffecStartDate",
                table: "Evaluation",
                columns: new[] { "RecieverAllocationPersonId", "RecieverAllocPersonEffecStartDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evaluation");
        }
    }
}
