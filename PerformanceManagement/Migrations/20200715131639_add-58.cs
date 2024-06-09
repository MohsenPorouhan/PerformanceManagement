using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add58 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CocherId",
                table: "FinalizeCalculation",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "FinalizeCalculation",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 15, 17, 46, 39, 184, DateTimeKind.Local).AddTicks(5801));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 15, 17, 46, 39, 186, DateTimeKind.Local).AddTicks(6513));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 15, 17, 46, 39, 186, DateTimeKind.Local).AddTicks(6532));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 15, 17, 46, 39, 186, DateTimeKind.Local).AddTicks(6537));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 15, 17, 46, 39, 191, DateTimeKind.Local).AddTicks(2663));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 15, 17, 46, 39, 191, DateTimeKind.Local).AddTicks(3457));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 15, 17, 46, 39, 191, DateTimeKind.Local).AddTicks(3474));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 15, 17, 46, 39, 191, DateTimeKind.Local).AddTicks(3479));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "FinalizeCalculation");

            migrationBuilder.AlterColumn<int>(
                name: "CocherId",
                table: "FinalizeCalculation",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 15, 10, 6, 30, 52, DateTimeKind.Local).AddTicks(1257));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 15, 10, 6, 30, 54, DateTimeKind.Local).AddTicks(9723));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 15, 10, 6, 30, 54, DateTimeKind.Local).AddTicks(9749));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 15, 10, 6, 30, 54, DateTimeKind.Local).AddTicks(9756));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 15, 10, 6, 30, 61, DateTimeKind.Local).AddTicks(9333));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 15, 10, 6, 30, 62, DateTimeKind.Local).AddTicks(831));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 15, 10, 6, 30, 62, DateTimeKind.Local).AddTicks(966));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 15, 10, 6, 30, 62, DateTimeKind.Local).AddTicks(975));
        }
    }
}
