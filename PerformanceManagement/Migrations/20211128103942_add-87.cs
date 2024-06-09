using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add87 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PeriodCode",
                table: "PeriodDefinitoion",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 11, 28, 14, 9, 41, 755, DateTimeKind.Local).AddTicks(5679));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 11, 28, 14, 9, 41, 758, DateTimeKind.Local).AddTicks(5469));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2021, 11, 28, 14, 9, 41, 758, DateTimeKind.Local).AddTicks(5481));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2021, 11, 28, 14, 9, 41, 758, DateTimeKind.Local).AddTicks(5484));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 11, 28, 14, 9, 41, 761, DateTimeKind.Local).AddTicks(4195));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 11, 28, 14, 9, 41, 761, DateTimeKind.Local).AddTicks(4676));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2021, 11, 28, 14, 9, 41, 761, DateTimeKind.Local).AddTicks(4687));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2021, 11, 28, 14, 9, 41, 761, DateTimeKind.Local).AddTicks(4690));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 11, 28, 14, 9, 41, 779, DateTimeKind.Local).AddTicks(3019));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 11, 28, 14, 9, 41, 779, DateTimeKind.Local).AddTicks(3511));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2021, 11, 28, 14, 9, 41, 779, DateTimeKind.Local).AddTicks(3521));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2021, 11, 28, 14, 9, 41, 779, DateTimeKind.Local).AddTicks(3524));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PeriodCode",
                table: "PeriodDefinitoion",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 10, 27, 8, 45, 21, 246, DateTimeKind.Local).AddTicks(1887));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 10, 27, 8, 45, 21, 247, DateTimeKind.Local).AddTicks(8774));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2021, 10, 27, 8, 45, 21, 247, DateTimeKind.Local).AddTicks(8786));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2021, 10, 27, 8, 45, 21, 247, DateTimeKind.Local).AddTicks(8788));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 10, 27, 8, 45, 21, 250, DateTimeKind.Local).AddTicks(9189));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 10, 27, 8, 45, 21, 251, DateTimeKind.Local).AddTicks(274));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2021, 10, 27, 8, 45, 21, 251, DateTimeKind.Local).AddTicks(285));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2021, 10, 27, 8, 45, 21, 251, DateTimeKind.Local).AddTicks(288));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 10, 27, 8, 45, 21, 270, DateTimeKind.Local).AddTicks(3404));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 10, 27, 8, 45, 21, 270, DateTimeKind.Local).AddTicks(3924));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2021, 10, 27, 8, 45, 21, 270, DateTimeKind.Local).AddTicks(3935));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2021, 10, 27, 8, 45, 21, 270, DateTimeKind.Local).AddTicks(3937));
        }
    }
}
