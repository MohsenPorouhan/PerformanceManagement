using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EvaluationHierarchyEffectiveStartDate",
                table: "Task",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EvaluationHierarchyId",
                table: "Task",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Task_EvaluationHierarchyId_EvaluationHierarchyEffectiveStartDate",
                table: "Task",
                columns: new[] { "EvaluationHierarchyId", "EvaluationHierarchyEffectiveStartDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_Task_evaluationHierarchies_EvaluationHierarchyId_EvaluationHierarchyEffectiveStartDate",
                table: "Task",
                columns: new[] { "EvaluationHierarchyId", "EvaluationHierarchyEffectiveStartDate" },
                principalTable: "evaluationHierarchies",
                principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_evaluationHierarchies_EvaluationHierarchyId_EvaluationHierarchyEffectiveStartDate",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_EvaluationHierarchyId_EvaluationHierarchyEffectiveStartDate",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "EvaluationHierarchyEffectiveStartDate",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "EvaluationHierarchyId",
                table: "Task");
        }
    }
}
