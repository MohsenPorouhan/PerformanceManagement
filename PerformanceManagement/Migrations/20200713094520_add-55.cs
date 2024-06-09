using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add55 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeriodDefinitionId",
                table: "EvaluationCompetencyCalculation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PeriodDefinitionId",
                table: "EvaluationCalculation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 14, 15, 20, 47, DateTimeKind.Local).AddTicks(2532));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 14, 15, 20, 52, DateTimeKind.Local).AddTicks(2821));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 14, 15, 20, 52, DateTimeKind.Local).AddTicks(2853));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 14, 15, 20, 52, DateTimeKind.Local).AddTicks(2861));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 14, 15, 20, 58, DateTimeKind.Local).AddTicks(9624));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 14, 15, 20, 59, DateTimeKind.Local).AddTicks(730));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 14, 15, 20, 59, DateTimeKind.Local).AddTicks(753));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 14, 15, 20, 59, DateTimeKind.Local).AddTicks(761));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeriodDefinitionId",
                table: "EvaluationCompetencyCalculation");

            migrationBuilder.DropColumn(
                name: "PeriodDefinitionId",
                table: "EvaluationCalculation");

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 10, 9, 41, 577, DateTimeKind.Local).AddTicks(6261));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 10, 9, 41, 586, DateTimeKind.Local).AddTicks(7678));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 10, 9, 41, 586, DateTimeKind.Local).AddTicks(7702));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 10, 9, 41, 586, DateTimeKind.Local).AddTicks(7708));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 10, 9, 41, 592, DateTimeKind.Local).AddTicks(7459));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 10, 9, 41, 592, DateTimeKind.Local).AddTicks(8378));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 10, 9, 41, 592, DateTimeKind.Local).AddTicks(8397));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 10, 9, 41, 592, DateTimeKind.Local).AddTicks(8402));
        }
    }
}
