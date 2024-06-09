using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add85 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CalculationWay",
                table: "Criteria",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CriteriaType",
                table: "Criteria",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 30, 16, 10, 23, 433, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 30, 16, 10, 23, 434, DateTimeKind.Local).AddTicks(7905));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 30, 16, 10, 23, 434, DateTimeKind.Local).AddTicks(7916));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 30, 16, 10, 23, 434, DateTimeKind.Local).AddTicks(7919));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 30, 16, 10, 23, 437, DateTimeKind.Local).AddTicks(4260));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 30, 16, 10, 23, 437, DateTimeKind.Local).AddTicks(4785));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 30, 16, 10, 23, 437, DateTimeKind.Local).AddTicks(4795));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 30, 16, 10, 23, 437, DateTimeKind.Local).AddTicks(4798));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 30, 16, 10, 23, 455, DateTimeKind.Local).AddTicks(8137));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 30, 16, 10, 23, 455, DateTimeKind.Local).AddTicks(8645));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 30, 16, 10, 23, 455, DateTimeKind.Local).AddTicks(8656));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 30, 16, 10, 23, 455, DateTimeKind.Local).AddTicks(8658));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalculationWay",
                table: "Criteria");

            migrationBuilder.DropColumn(
                name: "CriteriaType",
                table: "Criteria");

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
        }
    }
}
