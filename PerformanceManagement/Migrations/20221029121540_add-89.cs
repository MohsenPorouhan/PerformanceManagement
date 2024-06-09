using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add89 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProcessingCriteria",
                table: "Criteria",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProcessingCriteria",
                table: "Criteria");

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 1, 17, 11, 32, 14, 553, DateTimeKind.Local).AddTicks(8559));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 1, 17, 11, 32, 14, 555, DateTimeKind.Local).AddTicks(1258));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 1, 17, 11, 32, 14, 555, DateTimeKind.Local).AddTicks(1268));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2022, 1, 17, 11, 32, 14, 555, DateTimeKind.Local).AddTicks(1270));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 1, 17, 11, 32, 14, 557, DateTimeKind.Local).AddTicks(6309));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 1, 17, 11, 32, 14, 557, DateTimeKind.Local).AddTicks(6770));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 1, 17, 11, 32, 14, 557, DateTimeKind.Local).AddTicks(6781));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2022, 1, 17, 11, 32, 14, 557, DateTimeKind.Local).AddTicks(6783));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 1, 17, 11, 32, 14, 576, DateTimeKind.Local).AddTicks(6290));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 1, 17, 11, 32, 14, 576, DateTimeKind.Local).AddTicks(6812));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 1, 17, 11, 32, 14, 576, DateTimeKind.Local).AddTicks(6822));

            migrationBuilder.UpdateData(
                table: "ScoreScheduleType",
                keyColumn: "ScoreScheduleTypeId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2022, 1, 17, 11, 32, 14, 576, DateTimeKind.Local).AddTicks(6824));
        }
    }
}
