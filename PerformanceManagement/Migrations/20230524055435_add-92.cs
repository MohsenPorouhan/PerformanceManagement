using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add92 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriorCriteriaId",
                table: "Criteria",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 24, 10, 24, 34, 670, DateTimeKind.Local).AddTicks(1253));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 24, 10, 24, 34, 671, DateTimeKind.Local).AddTicks(5148));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 24, 10, 24, 34, 671, DateTimeKind.Local).AddTicks(5159));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 24, 10, 24, 34, 671, DateTimeKind.Local).AddTicks(5161));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 24, 10, 24, 34, 674, DateTimeKind.Local).AddTicks(2009));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 24, 10, 24, 34, 674, DateTimeKind.Local).AddTicks(2323));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 24, 10, 24, 34, 674, DateTimeKind.Local).AddTicks(2333));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 24, 10, 24, 34, 674, DateTimeKind.Local).AddTicks(2336));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 24, 10, 24, 34, 693, DateTimeKind.Local).AddTicks(3327));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 24, 10, 24, 34, 693, DateTimeKind.Local).AddTicks(3648));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 24, 10, 24, 34, 693, DateTimeKind.Local).AddTicks(3658));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 24, 10, 24, 34, 693, DateTimeKind.Local).AddTicks(3664));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriorCriteriaId",
                table: "Criteria");

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 21, 17, 31, 24, 413, DateTimeKind.Local).AddTicks(7073));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 21, 17, 31, 24, 415, DateTimeKind.Local).AddTicks(1951));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 21, 17, 31, 24, 415, DateTimeKind.Local).AddTicks(1962));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 21, 17, 31, 24, 415, DateTimeKind.Local).AddTicks(1965));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 21, 17, 31, 24, 417, DateTimeKind.Local).AddTicks(5494));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 21, 17, 31, 24, 417, DateTimeKind.Local).AddTicks(5801));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 21, 17, 31, 24, 417, DateTimeKind.Local).AddTicks(5810));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 21, 17, 31, 24, 417, DateTimeKind.Local).AddTicks(5813));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 21, 17, 31, 24, 433, DateTimeKind.Local).AddTicks(4800));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 21, 17, 31, 24, 433, DateTimeKind.Local).AddTicks(5101));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 21, 17, 31, 24, 433, DateTimeKind.Local).AddTicks(5111));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 21, 17, 31, 24, 433, DateTimeKind.Local).AddTicks(5113));
        }
    }
}
