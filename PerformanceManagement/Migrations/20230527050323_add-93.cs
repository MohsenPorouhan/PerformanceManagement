using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add93 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPriorPeriodTransition",
                table: "EvaluationBehaviouralCompetency",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PriorEvaluationId",
                table: "EvaluationBehaviouralCompetency",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PriorPeriodDefinitionId",
                table: "EvaluationBehaviouralCompetency",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 27, 9, 33, 22, 805, DateTimeKind.Local).AddTicks(2642));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 27, 9, 33, 22, 806, DateTimeKind.Local).AddTicks(7602));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 27, 9, 33, 22, 806, DateTimeKind.Local).AddTicks(7613));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 27, 9, 33, 22, 806, DateTimeKind.Local).AddTicks(7616));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 27, 9, 33, 22, 809, DateTimeKind.Local).AddTicks(5671));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 27, 9, 33, 22, 809, DateTimeKind.Local).AddTicks(5973));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 27, 9, 33, 22, 809, DateTimeKind.Local).AddTicks(5982));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 27, 9, 33, 22, 809, DateTimeKind.Local).AddTicks(5984));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 27, 9, 33, 22, 825, DateTimeKind.Local).AddTicks(5590));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 27, 9, 33, 22, 825, DateTimeKind.Local).AddTicks(6036));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 27, 9, 33, 22, 825, DateTimeKind.Local).AddTicks(6045));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 27, 9, 33, 22, 825, DateTimeKind.Local).AddTicks(6047));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPriorPeriodTransition",
                table: "EvaluationBehaviouralCompetency");

            migrationBuilder.DropColumn(
                name: "PriorEvaluationId",
                table: "EvaluationBehaviouralCompetency");

            migrationBuilder.DropColumn(
                name: "PriorPeriodDefinitionId",
                table: "EvaluationBehaviouralCompetency");

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
    }
}
