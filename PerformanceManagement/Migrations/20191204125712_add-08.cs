using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add08 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonPeriodEvaluationHierarchy_PeriodDefinitoion_PeriodDefinitoionId",
                table: "PersonPeriodEvaluationHierarchy");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonPeriodEvaluationHierarchy_evaluationHierarchies_EvaluationHierarchyId_EvaluationHierarchyEffectiveStartDate",
                table: "PersonPeriodEvaluationHierarchy");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonPeriodEvaluationHierarchy_People_PeopleId_PeopleEffectiveStartDate",
                table: "PersonPeriodEvaluationHierarchy");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionPeriod_PeriodDefinitoion_PeriodDefinitoionId",
                table: "SectionPeriod");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SectionPeriod",
                table: "SectionPeriod");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonPeriodEvaluationHierarchy",
                table: "PersonPeriodEvaluationHierarchy");

            migrationBuilder.RenameTable(
                name: "SectionPeriod",
                newName: "SectionPeriods");

            migrationBuilder.RenameTable(
                name: "PersonPeriodEvaluationHierarchy",
                newName: "PersonPeriodEvaluationHierarchies");

            migrationBuilder.RenameIndex(
                name: "IX_SectionPeriod_PeriodDefinitoionId",
                table: "SectionPeriods",
                newName: "IX_SectionPeriods_PeriodDefinitoionId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonPeriodEvaluationHierarchy_PeopleId_PeopleEffectiveStartDate",
                table: "PersonPeriodEvaluationHierarchies",
                newName: "IX_PersonPeriodEvaluationHierarchies_PeopleId_PeopleEffectiveStartDate");

            migrationBuilder.RenameIndex(
                name: "IX_PersonPeriodEvaluationHierarchy_EvaluationHierarchyId_EvaluationHierarchyEffectiveStartDate",
                table: "PersonPeriodEvaluationHierarchies",
                newName: "IX_PersonPeriodEvaluationHierarchies_EvaluationHierarchyId_EvaluationHierarchyEffectiveStartDate");

            migrationBuilder.RenameIndex(
                name: "IX_PersonPeriodEvaluationHierarchy_PeriodDefinitoionId",
                table: "PersonPeriodEvaluationHierarchies",
                newName: "IX_PersonPeriodEvaluationHierarchies_PeriodDefinitoionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SectionPeriods",
                table: "SectionPeriods",
                column: "SectionPeriodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonPeriodEvaluationHierarchies",
                table: "PersonPeriodEvaluationHierarchies",
                column: "PersonPeriodEvaluationHierarchyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonPeriodEvaluationHierarchies_PeriodDefinitoion_PeriodDefinitoionId",
                table: "PersonPeriodEvaluationHierarchies",
                column: "PeriodDefinitoionId",
                principalTable: "PeriodDefinitoion",
                principalColumn: "PeriodDefinitoionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonPeriodEvaluationHierarchies_evaluationHierarchies_EvaluationHierarchyId_EvaluationHierarchyEffectiveStartDate",
                table: "PersonPeriodEvaluationHierarchies",
                columns: new[] { "EvaluationHierarchyId", "EvaluationHierarchyEffectiveStartDate" },
                principalTable: "evaluationHierarchies",
                principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonPeriodEvaluationHierarchies_People_PeopleId_PeopleEffectiveStartDate",
                table: "PersonPeriodEvaluationHierarchies",
                columns: new[] { "PeopleId", "PeopleEffectiveStartDate" },
                principalTable: "People",
                principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionPeriods_PeriodDefinitoion_PeriodDefinitoionId",
                table: "SectionPeriods",
                column: "PeriodDefinitoionId",
                principalTable: "PeriodDefinitoion",
                principalColumn: "PeriodDefinitoionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonPeriodEvaluationHierarchies_PeriodDefinitoion_PeriodDefinitoionId",
                table: "PersonPeriodEvaluationHierarchies");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonPeriodEvaluationHierarchies_evaluationHierarchies_EvaluationHierarchyId_EvaluationHierarchyEffectiveStartDate",
                table: "PersonPeriodEvaluationHierarchies");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonPeriodEvaluationHierarchies_People_PeopleId_PeopleEffectiveStartDate",
                table: "PersonPeriodEvaluationHierarchies");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionPeriods_PeriodDefinitoion_PeriodDefinitoionId",
                table: "SectionPeriods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SectionPeriods",
                table: "SectionPeriods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonPeriodEvaluationHierarchies",
                table: "PersonPeriodEvaluationHierarchies");

            migrationBuilder.RenameTable(
                name: "SectionPeriods",
                newName: "SectionPeriod");

            migrationBuilder.RenameTable(
                name: "PersonPeriodEvaluationHierarchies",
                newName: "PersonPeriodEvaluationHierarchy");

            migrationBuilder.RenameIndex(
                name: "IX_SectionPeriods_PeriodDefinitoionId",
                table: "SectionPeriod",
                newName: "IX_SectionPeriod_PeriodDefinitoionId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonPeriodEvaluationHierarchies_PeopleId_PeopleEffectiveStartDate",
                table: "PersonPeriodEvaluationHierarchy",
                newName: "IX_PersonPeriodEvaluationHierarchy_PeopleId_PeopleEffectiveStartDate");

            migrationBuilder.RenameIndex(
                name: "IX_PersonPeriodEvaluationHierarchies_EvaluationHierarchyId_EvaluationHierarchyEffectiveStartDate",
                table: "PersonPeriodEvaluationHierarchy",
                newName: "IX_PersonPeriodEvaluationHierarchy_EvaluationHierarchyId_EvaluationHierarchyEffectiveStartDate");

            migrationBuilder.RenameIndex(
                name: "IX_PersonPeriodEvaluationHierarchies_PeriodDefinitoionId",
                table: "PersonPeriodEvaluationHierarchy",
                newName: "IX_PersonPeriodEvaluationHierarchy_PeriodDefinitoionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SectionPeriod",
                table: "SectionPeriod",
                column: "SectionPeriodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonPeriodEvaluationHierarchy",
                table: "PersonPeriodEvaluationHierarchy",
                column: "PersonPeriodEvaluationHierarchyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonPeriodEvaluationHierarchy_PeriodDefinitoion_PeriodDefinitoionId",
                table: "PersonPeriodEvaluationHierarchy",
                column: "PeriodDefinitoionId",
                principalTable: "PeriodDefinitoion",
                principalColumn: "PeriodDefinitoionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonPeriodEvaluationHierarchy_evaluationHierarchies_EvaluationHierarchyId_EvaluationHierarchyEffectiveStartDate",
                table: "PersonPeriodEvaluationHierarchy",
                columns: new[] { "EvaluationHierarchyId", "EvaluationHierarchyEffectiveStartDate" },
                principalTable: "evaluationHierarchies",
                principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonPeriodEvaluationHierarchy_People_PeopleId_PeopleEffectiveStartDate",
                table: "PersonPeriodEvaluationHierarchy",
                columns: new[] { "PeopleId", "PeopleEffectiveStartDate" },
                principalTable: "People",
                principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionPeriod_PeriodDefinitoion_PeriodDefinitoionId",
                table: "SectionPeriod",
                column: "PeriodDefinitoionId",
                principalTable: "PeriodDefinitoion",
                principalColumn: "PeriodDefinitoionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
