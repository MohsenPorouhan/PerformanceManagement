using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add74 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoacherTruth",
                columns: table => new
                {
                    CoacherTruthId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    BehaviouralCompetencyId = table.Column<int>(nullable: false),
                    PeriodDefinitionId = table.Column<int>(nullable: false),
                    CoacherId = table.Column<int>(nullable: false),
                    CoacherEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    CoacherDepartmentId = table.Column<int>(nullable: false),
                    CoacherDepartmenEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoacherTruth", x => x.CoacherTruthId);
                    table.ForeignKey(
                        name: "FK_CoacherTruth_BehaviouralCompetency_BehaviouralCompetencyId",
                        column: x => x.BehaviouralCompetencyId,
                        principalTable: "BehaviouralCompetency",
                        principalColumn: "BehaviouralCompetencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoacherTruth_PeriodDefinitoion_PeriodDefinitionId",
                        column: x => x.PeriodDefinitionId,
                        principalTable: "PeriodDefinitoion",
                        principalColumn: "PeriodDefinitoionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoacherTruth_evaluationHierarchies_CoacherDepartmentId_CoacherDepartmenEffectiveStartDate",
                        columns: x => new { x.CoacherDepartmentId, x.CoacherDepartmenEffectiveStartDate },
                        principalTable: "evaluationHierarchies",
                        principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoacherTruth_People_CoacherId_CoacherEffectiveStartDate",
                        columns: x => new { x.CoacherId, x.CoacherEffectiveStartDate },
                        principalTable: "People",
                        principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 10, 19, 10, 23, 18, 318, DateTimeKind.Local).AddTicks(7877));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 10, 19, 10, 23, 18, 326, DateTimeKind.Local).AddTicks(4045));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 10, 19, 10, 23, 18, 326, DateTimeKind.Local).AddTicks(4077));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 10, 19, 10, 23, 18, 326, DateTimeKind.Local).AddTicks(4086));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 10, 19, 10, 23, 18, 336, DateTimeKind.Local).AddTicks(3838));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 10, 19, 10, 23, 18, 336, DateTimeKind.Local).AddTicks(5088));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 10, 19, 10, 23, 18, 336, DateTimeKind.Local).AddTicks(5115));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 10, 19, 10, 23, 18, 336, DateTimeKind.Local).AddTicks(5125));

            migrationBuilder.CreateIndex(
                name: "IX_CoacherTruth_BehaviouralCompetencyId",
                table: "CoacherTruth",
                column: "BehaviouralCompetencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CoacherTruth_PeriodDefinitionId",
                table: "CoacherTruth",
                column: "PeriodDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_CoacherTruth_CoacherDepartmentId_CoacherDepartmenEffectiveStartDate",
                table: "CoacherTruth",
                columns: new[] { "CoacherDepartmentId", "CoacherDepartmenEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_CoacherTruth_CoacherId_CoacherEffectiveStartDate",
                table: "CoacherTruth",
                columns: new[] { "CoacherId", "CoacherEffectiveStartDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoacherTruth");

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 9, 6, 11, 0, 54, 161, DateTimeKind.Local).AddTicks(1002));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 9, 6, 11, 0, 54, 164, DateTimeKind.Local).AddTicks(6793));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 9, 6, 11, 0, 54, 164, DateTimeKind.Local).AddTicks(6825));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 9, 6, 11, 0, 54, 164, DateTimeKind.Local).AddTicks(6834));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 9, 6, 11, 0, 54, 174, DateTimeKind.Local).AddTicks(4498));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 9, 6, 11, 0, 54, 174, DateTimeKind.Local).AddTicks(5929));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 9, 6, 11, 0, 54, 174, DateTimeKind.Local).AddTicks(5961));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 9, 6, 11, 0, 54, 174, DateTimeKind.Local).AddTicks(5970));
        }
    }
}
