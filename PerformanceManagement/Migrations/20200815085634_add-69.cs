using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add69 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "FinalTaskScore",
                table: "MultipleTaskCoacherOfEmployeeFinalCalc",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "FinalCompetencyScore",
                table: "MultipleCompetencyCoacherOfEmployeeFinalCalc",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 13, 26, 32, 523, DateTimeKind.Local).AddTicks(9871));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 13, 26, 32, 531, DateTimeKind.Local).AddTicks(2201));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 13, 26, 32, 531, DateTimeKind.Local).AddTicks(2244));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 13, 26, 32, 531, DateTimeKind.Local).AddTicks(2256));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 13, 26, 32, 542, DateTimeKind.Local).AddTicks(1845));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 13, 26, 32, 542, DateTimeKind.Local).AddTicks(3717));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 13, 26, 32, 542, DateTimeKind.Local).AddTicks(3756));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 13, 26, 32, 542, DateTimeKind.Local).AddTicks(3766));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FinalTaskScore",
                table: "MultipleTaskCoacherOfEmployeeFinalCalc",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FinalCompetencyScore",
                table: "MultipleCompetencyCoacherOfEmployeeFinalCalc",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 10, 52, 20, 823, DateTimeKind.Local).AddTicks(7351));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 10, 52, 20, 827, DateTimeKind.Local).AddTicks(7755));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 10, 52, 20, 827, DateTimeKind.Local).AddTicks(7775));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 10, 52, 20, 827, DateTimeKind.Local).AddTicks(7780));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 10, 52, 20, 833, DateTimeKind.Local).AddTicks(6674));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 10, 52, 20, 833, DateTimeKind.Local).AddTicks(7780));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 10, 52, 20, 833, DateTimeKind.Local).AddTicks(7812));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 10, 52, 20, 833, DateTimeKind.Local).AddTicks(7818));
        }
    }
}
