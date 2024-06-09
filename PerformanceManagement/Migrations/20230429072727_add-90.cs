using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add90 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdNumber",
                table: "AspNetUsers",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdNumber",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 29, 15, 45, 39, 645, DateTimeKind.Local).AddTicks(9328));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 29, 15, 45, 39, 647, DateTimeKind.Local).AddTicks(1778));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 29, 15, 45, 39, 647, DateTimeKind.Local).AddTicks(1789));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 29, 15, 45, 39, 647, DateTimeKind.Local).AddTicks(1791));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 29, 15, 45, 39, 650, DateTimeKind.Local).AddTicks(7727));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 29, 15, 45, 39, 650, DateTimeKind.Local).AddTicks(8307));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 29, 15, 45, 39, 650, DateTimeKind.Local).AddTicks(8317));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 29, 15, 45, 39, 650, DateTimeKind.Local).AddTicks(8320));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 29, 15, 45, 39, 669, DateTimeKind.Local).AddTicks(4481));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 29, 15, 45, 39, 669, DateTimeKind.Local).AddTicks(5035));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 29, 15, 45, 39, 669, DateTimeKind.Local).AddTicks(5046));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 29, 15, 45, 39, 669, DateTimeKind.Local).AddTicks(5048));
        }
    }
}
