using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add91 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPriorPeriodTransition",
                table: "Evaluation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PriorEvaluationId",
                table: "Evaluation",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PriorPeriodDefinitionId",
                table: "Evaluation",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPriorPeriodTransition",
                table: "Evaluation");

            migrationBuilder.DropColumn(
                name: "PriorEvaluationId",
                table: "Evaluation");

            migrationBuilder.DropColumn(
                name: "PriorPeriodDefinitionId",
                table: "Evaluation");

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 4, 29, 11, 57, 27, 196, DateTimeKind.Local).AddTicks(8231));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 4, 29, 11, 57, 27, 198, DateTimeKind.Local).AddTicks(2158));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 4, 29, 11, 57, 27, 198, DateTimeKind.Local).AddTicks(2168));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 4, 29, 11, 57, 27, 198, DateTimeKind.Local).AddTicks(2171));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 4, 29, 11, 57, 27, 200, DateTimeKind.Local).AddTicks(6065));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 4, 29, 11, 57, 27, 200, DateTimeKind.Local).AddTicks(6351));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 4, 29, 11, 57, 27, 200, DateTimeKind.Local).AddTicks(6360));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 4, 29, 11, 57, 27, 200, DateTimeKind.Local).AddTicks(6363));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 4, 29, 11, 57, 27, 218, DateTimeKind.Local).AddTicks(8211));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 4, 29, 11, 57, 27, 218, DateTimeKind.Local).AddTicks(8520));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 4, 29, 11, 57, 27, 218, DateTimeKind.Local).AddTicks(8530));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 4, 29, 11, 57, 27, 218, DateTimeKind.Local).AddTicks(8533));
        }
    }
}
