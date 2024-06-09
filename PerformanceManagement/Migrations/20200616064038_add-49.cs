using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add49 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvaluationCompetencyCalculation",
                columns: table => new
                {
                    EvaluationCompetencyCalculationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EvaluationBehaviouralCompetencyId = table.Column<int>(nullable: false),
                    CalculatedCompetencyScore = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationCompetencyCalculation", x => x.EvaluationCompetencyCalculationId);
                    table.ForeignKey(
                        name: "FK_EvaluationCompetencyCalculation_EvaluationBehaviouralCompetency_EvaluationBehaviouralCompetencyId",
                        column: x => x.EvaluationBehaviouralCompetencyId,
                        principalTable: "EvaluationBehaviouralCompetency",
                        principalColumn: "EvaluationBehaviouralCompetencyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinalScoreCompetencyCalculation",
                columns: table => new
                {
                    FinalScoreCompetencyCalculationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllocatorEvalHieEffcStartDate = table.Column<DateTime>(nullable: true),
                    AllocatorEvaluationHierarchyId = table.Column<int>(nullable: false),
                    RecieverAllocEvalHieEffcStartDate = table.Column<DateTime>(nullable: true),
                    RecieverAllocationEvaluationHierarchyId = table.Column<int>(nullable: false),
                    AllocatorPersonEffecStartDate = table.Column<DateTime>(nullable: true),
                    AllocatorPersonId = table.Column<int>(nullable: false),
                    RecieverAllocPersonEffecStartDate = table.Column<DateTime>(nullable: true),
                    RecieverAllocationPersonId = table.Column<int>(nullable: false),
                    PeriodDefinitoionId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    AllocatorLevel = table.Column<int>(nullable: false),
                    CoacherType = table.Column<int>(nullable: false),
                    FinalCompetneciesScore = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalScoreCompetencyCalculation", x => x.FinalScoreCompetencyCalculationId);
                    table.ForeignKey(
                        name: "FK_FinalScoreCompetencyCalculation_PeriodDefinitoion_PeriodDefinitoionId",
                        column: x => x.PeriodDefinitoionId,
                        principalTable: "PeriodDefinitoion",
                        principalColumn: "PeriodDefinitoionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinalScoreCompetencyCalculation_evaluationHierarchies_AllocatorEvaluationHierarchyId_AllocatorEvalHieEffcStartDate",
                        columns: x => new { x.AllocatorEvaluationHierarchyId, x.AllocatorEvalHieEffcStartDate },
                        principalTable: "evaluationHierarchies",
                        principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinalScoreCompetencyCalculation_People_AllocatorPersonId_AllocatorPersonEffecStartDate",
                        columns: x => new { x.AllocatorPersonId, x.AllocatorPersonEffecStartDate },
                        principalTable: "People",
                        principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinalScoreCompetencyCalculation_evaluationHierarchies_RecieverAllocationEvaluationHierarchyId_RecieverAllocEvalHieEffcStartD~",
                        columns: x => new { x.RecieverAllocationEvaluationHierarchyId, x.RecieverAllocEvalHieEffcStartDate },
                        principalTable: "evaluationHierarchies",
                        principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinalScoreCompetencyCalculation_People_RecieverAllocationPersonId_RecieverAllocPersonEffecStartDate",
                        columns: x => new { x.RecieverAllocationPersonId, x.RecieverAllocPersonEffecStartDate },
                        principalTable: "People",
                        principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 16, 11, 10, 38, 193, DateTimeKind.Local).AddTicks(3146));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 16, 11, 10, 38, 194, DateTimeKind.Local).AddTicks(7806));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 16, 11, 10, 38, 194, DateTimeKind.Local).AddTicks(7819));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 16, 11, 10, 38, 194, DateTimeKind.Local).AddTicks(7822));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 16, 11, 10, 38, 199, DateTimeKind.Local).AddTicks(2148));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 16, 11, 10, 38, 199, DateTimeKind.Local).AddTicks(2699));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 16, 11, 10, 38, 199, DateTimeKind.Local).AddTicks(2710));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 16, 11, 10, 38, 199, DateTimeKind.Local).AddTicks(2713));

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationCompetencyCalculation_EvaluationBehaviouralCompetencyId",
                table: "EvaluationCompetencyCalculation",
                column: "EvaluationBehaviouralCompetencyId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalScoreCompetencyCalculation_PeriodDefinitoionId",
                table: "FinalScoreCompetencyCalculation",
                column: "PeriodDefinitoionId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalScoreCompetencyCalculation_AllocatorEvaluationHierarchyId_AllocatorEvalHieEffcStartDate",
                table: "FinalScoreCompetencyCalculation",
                columns: new[] { "AllocatorEvaluationHierarchyId", "AllocatorEvalHieEffcStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_FinalScoreCompetencyCalculation_AllocatorPersonId_AllocatorPersonEffecStartDate",
                table: "FinalScoreCompetencyCalculation",
                columns: new[] { "AllocatorPersonId", "AllocatorPersonEffecStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_FinalScoreCompetencyCalculation_RecieverAllocationEvaluationHierarchyId_RecieverAllocEvalHieEffcStartDate",
                table: "FinalScoreCompetencyCalculation",
                columns: new[] { "RecieverAllocationEvaluationHierarchyId", "RecieverAllocEvalHieEffcStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_FinalScoreCompetencyCalculation_RecieverAllocationPersonId_RecieverAllocPersonEffecStartDate",
                table: "FinalScoreCompetencyCalculation",
                columns: new[] { "RecieverAllocationPersonId", "RecieverAllocPersonEffecStartDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationCompetencyCalculation");

            migrationBuilder.DropTable(
                name: "FinalScoreCompetencyCalculation");

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 15, 9, 52, 11, 99, DateTimeKind.Local).AddTicks(1701));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 15, 9, 52, 11, 101, DateTimeKind.Local).AddTicks(2088));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 15, 9, 52, 11, 101, DateTimeKind.Local).AddTicks(2105));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 15, 9, 52, 11, 101, DateTimeKind.Local).AddTicks(2110));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 15, 9, 52, 11, 106, DateTimeKind.Local).AddTicks(3481));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 15, 9, 52, 11, 106, DateTimeKind.Local).AddTicks(4053));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 15, 9, 52, 11, 106, DateTimeKind.Local).AddTicks(4065));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 15, 9, 52, 11, 106, DateTimeKind.Local).AddTicks(4068));
        }
    }
}
