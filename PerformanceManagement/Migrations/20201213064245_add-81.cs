using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add81 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExtendScoreSchedule",
                columns: table => new
                {
                    ExtendScoreScheduleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ScoreScheduleTypeId = table.Column<int>(nullable: false),
                    DateFrom = table.Column<DateTime>(nullable: false),
                    DateTo = table.Column<DateTime>(nullable: false),
                    ExtendSectionPeriodId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtendScoreSchedule", x => x.ExtendScoreScheduleId);
                    table.ForeignKey(
                        name: "FK_ExtendScoreSchedule_ExtendSectionPeriod_ExtendSectionPeriodId",
                        column: x => x.ExtendSectionPeriodId,
                        principalTable: "ExtendSectionPeriod",
                        principalColumn: "ExtendSectionPeriodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExtendScoreSchedule_ScoreScheduleType_ScoreScheduleTypeId",
                        column: x => x.ScoreScheduleTypeId,
                        principalTable: "ScoreScheduleType",
                        principalColumn: "ScoreScheduleTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 13, 10, 12, 43, 754, DateTimeKind.Local).AddTicks(7293));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 13, 10, 12, 43, 760, DateTimeKind.Local).AddTicks(6952));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 13, 10, 12, 43, 760, DateTimeKind.Local).AddTicks(6976));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 13, 10, 12, 43, 760, DateTimeKind.Local).AddTicks(6982));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 13, 10, 12, 43, 768, DateTimeKind.Local).AddTicks(5873));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 13, 10, 12, 43, 768, DateTimeKind.Local).AddTicks(6934));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 13, 10, 12, 43, 768, DateTimeKind.Local).AddTicks(6955));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 13, 10, 12, 43, 768, DateTimeKind.Local).AddTicks(6960));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 13, 10, 12, 43, 921, DateTimeKind.Local).AddTicks(5775));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 13, 10, 12, 43, 921, DateTimeKind.Local).AddTicks(6766));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 13, 10, 12, 43, 921, DateTimeKind.Local).AddTicks(6788));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 13, 10, 12, 43, 921, DateTimeKind.Local).AddTicks(6792));

            migrationBuilder.CreateIndex(
                name: "IX_ExtendScoreSchedule_ExtendSectionPeriodId",
                table: "ExtendScoreSchedule",
                column: "ExtendSectionPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtendScoreSchedule_ScoreScheduleTypeId",
                table: "ExtendScoreSchedule",
                column: "ScoreScheduleTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtendScoreSchedule");

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 8, 14, 26, 40, 965, DateTimeKind.Local).AddTicks(6598));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 8, 14, 26, 40, 970, DateTimeKind.Local).AddTicks(2406));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 8, 14, 26, 40, 970, DateTimeKind.Local).AddTicks(2421));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 8, 14, 26, 40, 970, DateTimeKind.Local).AddTicks(2424));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 8, 14, 26, 40, 978, DateTimeKind.Local).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 8, 14, 26, 40, 978, DateTimeKind.Local).AddTicks(2435));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 8, 14, 26, 40, 978, DateTimeKind.Local).AddTicks(2448));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 8, 14, 26, 40, 978, DateTimeKind.Local).AddTicks(2451));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 8, 14, 26, 41, 8, DateTimeKind.Local).AddTicks(74));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 8, 14, 26, 41, 8, DateTimeKind.Local).AddTicks(1007));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 8, 14, 26, 41, 8, DateTimeKind.Local).AddTicks(1026));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 8, 14, 26, 41, 8, DateTimeKind.Local).AddTicks(1031));
        }
    }
}
