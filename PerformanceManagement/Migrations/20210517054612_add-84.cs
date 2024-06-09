using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add84 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChartInfo",
                columns: table => new
                {
                    ChartInfoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PeopleId = table.Column<int>(nullable: false),
                    PeopleEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    EvaluationHierarchyId = table.Column<int>(nullable: false),
                    EvalEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    Chairmanship = table.Column<string>(nullable: true),
                    Management = table.Column<string>(nullable: true),
                    VicePresident = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Intermediary = table.Column<bool>(nullable: false),
                    EffectiveStartDate = table.Column<DateTime>(nullable: false),
                    EffectiveEndDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedById = table.Column<int>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChartInfo", x => x.ChartInfoId);
                    table.ForeignKey(
                        name: "FK_ChartInfo_evaluationHierarchies_EvaluationHierarchyId_EvalEffectiveStartDate",
                        columns: x => new { x.EvaluationHierarchyId, x.EvalEffectiveStartDate },
                        principalTable: "evaluationHierarchies",
                        principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChartInfo_People_PeopleId_PeopleEffectiveStartDate",
                        columns: x => new { x.PeopleId, x.PeopleEffectiveStartDate },
                        principalTable: "People",
                        principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 5, 17, 10, 16, 10, 850, DateTimeKind.Local).AddTicks(211));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 5, 17, 10, 16, 10, 858, DateTimeKind.Local).AddTicks(5357));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2021, 5, 17, 10, 16, 10, 858, DateTimeKind.Local).AddTicks(5365));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2021, 5, 17, 10, 16, 10, 858, DateTimeKind.Local).AddTicks(5371));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 5, 17, 10, 16, 10, 871, DateTimeKind.Local).AddTicks(6476));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 5, 17, 10, 16, 10, 871, DateTimeKind.Local).AddTicks(6514));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2021, 5, 17, 10, 16, 10, 871, DateTimeKind.Local).AddTicks(6521));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2021, 5, 17, 10, 16, 10, 871, DateTimeKind.Local).AddTicks(6528));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 5, 17, 10, 16, 10, 954, DateTimeKind.Local).AddTicks(3281));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 5, 17, 10, 16, 10, 954, DateTimeKind.Local).AddTicks(3321));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2021, 5, 17, 10, 16, 10, 954, DateTimeKind.Local).AddTicks(3327));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2021, 5, 17, 10, 16, 10, 954, DateTimeKind.Local).AddTicks(3333));

            migrationBuilder.CreateIndex(
                name: "IX_ChartInfo_EvaluationHierarchyId_EvalEffectiveStartDate",
                table: "ChartInfo",
                columns: new[] { "EvaluationHierarchyId", "EvalEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_ChartInfo_PeopleId_PeopleEffectiveStartDate",
                table: "ChartInfo",
                columns: new[] { "PeopleId", "PeopleEffectiveStartDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChartInfo");

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 3, 15, 11, 32, 7, 963, DateTimeKind.Local).AddTicks(7993));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 3, 15, 11, 32, 7, 969, DateTimeKind.Local).AddTicks(9081));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2021, 3, 15, 11, 32, 7, 969, DateTimeKind.Local).AddTicks(9108));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2021, 3, 15, 11, 32, 7, 969, DateTimeKind.Local).AddTicks(9113));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 3, 15, 11, 32, 7, 976, DateTimeKind.Local).AddTicks(2750));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 3, 15, 11, 32, 7, 976, DateTimeKind.Local).AddTicks(3421));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2021, 3, 15, 11, 32, 7, 976, DateTimeKind.Local).AddTicks(3434));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2021, 3, 15, 11, 32, 7, 976, DateTimeKind.Local).AddTicks(3437));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 3, 15, 11, 32, 8, 17, DateTimeKind.Local).AddTicks(5235));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 3, 15, 11, 32, 8, 17, DateTimeKind.Local).AddTicks(5901));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2021, 3, 15, 11, 32, 8, 17, DateTimeKind.Local).AddTicks(5914));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2021, 3, 15, 11, 32, 8, 17, DateTimeKind.Local).AddTicks(5918));
        }
    }
}
