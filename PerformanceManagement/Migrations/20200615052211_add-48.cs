using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add48 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CriteriaCalculation",
                columns: table => new
                {
                    CriteriaCalculationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EvaluationId = table.Column<int>(nullable: false),
                    CriteriaWeightId = table.Column<int>(nullable: false),
                    CalculatedCriteriaScore = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriteriaCalculation", x => x.CriteriaCalculationId);
                    table.ForeignKey(
                        name: "FK_CriteriaCalculation_CriteriaWeight_CriteriaWeightId",
                        column: x => x.CriteriaWeightId,
                        principalTable: "CriteriaWeight",
                        principalColumn: "CriteriaWeightId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CriteriaCalculation_Evaluation_EvaluationId",
                        column: x => x.EvaluationId,
                        principalTable: "Evaluation",
                        principalColumn: "EvaluationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationCalculation",
                columns: table => new
                {
                    EvaluationCalculationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EvaluationId = table.Column<int>(nullable: false),
                    CalculatedScore = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationCalculation", x => x.EvaluationCalculationId);
                    table.ForeignKey(
                        name: "FK_EvaluationCalculation_Evaluation_EvaluationId",
                        column: x => x.EvaluationId,
                        principalTable: "Evaluation",
                        principalColumn: "EvaluationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinalScoreCalculation",
                columns: table => new
                {
                    FinalScoreCalculationId = table.Column<int>(nullable: false)
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
                    FinalScore = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalScoreCalculation", x => x.FinalScoreCalculationId);
                    table.ForeignKey(
                        name: "FK_FinalScoreCalculation_PeriodDefinitoion_PeriodDefinitoionId",
                        column: x => x.PeriodDefinitoionId,
                        principalTable: "PeriodDefinitoion",
                        principalColumn: "PeriodDefinitoionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinalScoreCalculation_evaluationHierarchies_AllocatorEvaluationHierarchyId_AllocatorEvalHieEffcStartDate",
                        columns: x => new { x.AllocatorEvaluationHierarchyId, x.AllocatorEvalHieEffcStartDate },
                        principalTable: "evaluationHierarchies",
                        principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinalScoreCalculation_People_AllocatorPersonId_AllocatorPersonEffecStartDate",
                        columns: x => new { x.AllocatorPersonId, x.AllocatorPersonEffecStartDate },
                        principalTable: "People",
                        principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinalScoreCalculation_evaluationHierarchies_RecieverAllocationEvaluationHierarchyId_RecieverAllocEvalHieEffcStartDate",
                        columns: x => new { x.RecieverAllocationEvaluationHierarchyId, x.RecieverAllocEvalHieEffcStartDate },
                        principalTable: "evaluationHierarchies",
                        principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinalScoreCalculation_People_RecieverAllocationPersonId_RecieverAllocPersonEffecStartDate",
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

            migrationBuilder.CreateIndex(
                name: "IX_CriteriaCalculation_CriteriaWeightId",
                table: "CriteriaCalculation",
                column: "CriteriaWeightId");

            migrationBuilder.CreateIndex(
                name: "IX_CriteriaCalculation_EvaluationId",
                table: "CriteriaCalculation",
                column: "EvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationCalculation_EvaluationId",
                table: "EvaluationCalculation",
                column: "EvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalScoreCalculation_PeriodDefinitoionId",
                table: "FinalScoreCalculation",
                column: "PeriodDefinitoionId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalScoreCalculation_AllocatorEvaluationHierarchyId_AllocatorEvalHieEffcStartDate",
                table: "FinalScoreCalculation",
                columns: new[] { "AllocatorEvaluationHierarchyId", "AllocatorEvalHieEffcStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_FinalScoreCalculation_AllocatorPersonId_AllocatorPersonEffecStartDate",
                table: "FinalScoreCalculation",
                columns: new[] { "AllocatorPersonId", "AllocatorPersonEffecStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_FinalScoreCalculation_RecieverAllocationEvaluationHierarchyId_RecieverAllocEvalHieEffcStartDate",
                table: "FinalScoreCalculation",
                columns: new[] { "RecieverAllocationEvaluationHierarchyId", "RecieverAllocEvalHieEffcStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_FinalScoreCalculation_RecieverAllocationPersonId_RecieverAllocPersonEffecStartDate",
                table: "FinalScoreCalculation",
                columns: new[] { "RecieverAllocationPersonId", "RecieverAllocPersonEffecStartDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CriteriaCalculation");

            migrationBuilder.DropTable(
                name: "EvaluationCalculation");

            migrationBuilder.DropTable(
                name: "FinalScoreCalculation");

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 13, 9, 34, 56, 646, DateTimeKind.Local).AddTicks(6120));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 13, 9, 34, 56, 650, DateTimeKind.Local).AddTicks(5489));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 13, 9, 34, 56, 650, DateTimeKind.Local).AddTicks(5501));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 13, 9, 34, 56, 650, DateTimeKind.Local).AddTicks(5504));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 13, 9, 34, 56, 654, DateTimeKind.Local).AddTicks(248));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 13, 9, 34, 56, 654, DateTimeKind.Local).AddTicks(807));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 13, 9, 34, 56, 654, DateTimeKind.Local).AddTicks(818));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 6, 13, 9, 34, 56, 654, DateTimeKind.Local).AddTicks(820));
        }
    }
}
