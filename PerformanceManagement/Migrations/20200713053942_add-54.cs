using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add54 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoacherDepartmentId",
                table: "EvaluationCompetencyCalculation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CoacherId",
                table: "EvaluationCompetencyCalculation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "EvaluationCompetencyCalculation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeDepartmentId",
                table: "EvaluationCompetencyCalculation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "EvaluationCompetencyCalculation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CoacherDepartmentId",
                table: "EvaluationCalculation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CoacherId",
                table: "EvaluationCalculation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "EvaluationCalculation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeDepartmentId",
                table: "EvaluationCalculation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "EvaluationCalculation",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoacherDepartmentId",
                table: "EvaluationCompetencyCalculation");

            migrationBuilder.DropColumn(
                name: "CoacherId",
                table: "EvaluationCompetencyCalculation");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "EvaluationCompetencyCalculation");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartmentId",
                table: "EvaluationCompetencyCalculation");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "EvaluationCompetencyCalculation");

            migrationBuilder.DropColumn(
                name: "CoacherDepartmentId",
                table: "EvaluationCalculation");

            migrationBuilder.DropColumn(
                name: "CoacherId",
                table: "EvaluationCalculation");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "EvaluationCalculation");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartmentId",
                table: "EvaluationCalculation");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "EvaluationCalculation");

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 11, 15, 14, 34, 512, DateTimeKind.Local).AddTicks(8381));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 11, 15, 14, 34, 520, DateTimeKind.Local).AddTicks(6916));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 11, 15, 14, 34, 520, DateTimeKind.Local).AddTicks(6938));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 11, 15, 14, 34, 520, DateTimeKind.Local).AddTicks(6942));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 11, 15, 14, 34, 527, DateTimeKind.Local).AddTicks(3127));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 11, 15, 14, 34, 527, DateTimeKind.Local).AddTicks(4279));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 11, 15, 14, 34, 527, DateTimeKind.Local).AddTicks(4305));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 11, 15, 14, 34, 527, DateTimeKind.Local).AddTicks(4313));
        }
    }
}
