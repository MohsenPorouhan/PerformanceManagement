using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add77 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CoacherDepartmentId",
                table: "CoacherTruth",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmployeeDepartmenEffectiveStartDate",
                table: "CoacherTruth",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeDepartmentId",
                table: "CoacherTruth",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmployeeEffectiveStartDate",
                table: "CoacherTruth",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "CoacherTruth",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 11, 4, 10, 47, 45, 417, DateTimeKind.Local).AddTicks(5987));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 11, 4, 10, 47, 45, 422, DateTimeKind.Local).AddTicks(4585));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 11, 4, 10, 47, 45, 422, DateTimeKind.Local).AddTicks(4603));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 11, 4, 10, 47, 45, 422, DateTimeKind.Local).AddTicks(4607));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 11, 4, 10, 47, 45, 429, DateTimeKind.Local).AddTicks(753));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 11, 4, 10, 47, 45, 429, DateTimeKind.Local).AddTicks(2117));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 11, 4, 10, 47, 45, 429, DateTimeKind.Local).AddTicks(2146));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 11, 4, 10, 47, 45, 429, DateTimeKind.Local).AddTicks(2154));

            migrationBuilder.CreateIndex(
                name: "IX_CoacherTruth_EmployeeDepartmentId_EmployeeDepartmenEffectiveStartDate",
                table: "CoacherTruth",
                columns: new[] { "EmployeeDepartmentId", "EmployeeDepartmenEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_CoacherTruth_EmployeeId_EmployeeEffectiveStartDate",
                table: "CoacherTruth",
                columns: new[] { "EmployeeId", "EmployeeEffectiveStartDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_CoacherTruth_evaluationHierarchies_EmployeeDepartmentId_EmployeeDepartmenEffectiveStartDate",
                table: "CoacherTruth",
                columns: new[] { "EmployeeDepartmentId", "EmployeeDepartmenEffectiveStartDate" },
                principalTable: "evaluationHierarchies",
                principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoacherTruth_People_EmployeeId_EmployeeEffectiveStartDate",
                table: "CoacherTruth",
                columns: new[] { "EmployeeId", "EmployeeEffectiveStartDate" },
                principalTable: "People",
                principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoacherTruth_evaluationHierarchies_EmployeeDepartmentId_EmployeeDepartmenEffectiveStartDate",
                table: "CoacherTruth");

            migrationBuilder.DropForeignKey(
                name: "FK_CoacherTruth_People_EmployeeId_EmployeeEffectiveStartDate",
                table: "CoacherTruth");

            migrationBuilder.DropIndex(
                name: "IX_CoacherTruth_EmployeeDepartmentId_EmployeeDepartmenEffectiveStartDate",
                table: "CoacherTruth");

            migrationBuilder.DropIndex(
                name: "IX_CoacherTruth_EmployeeId_EmployeeEffectiveStartDate",
                table: "CoacherTruth");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartmenEffectiveStartDate",
                table: "CoacherTruth");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartmentId",
                table: "CoacherTruth");

            migrationBuilder.DropColumn(
                name: "EmployeeEffectiveStartDate",
                table: "CoacherTruth");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "CoacherTruth");

            migrationBuilder.AlterColumn<int>(
                name: "CoacherDepartmentId",
                table: "CoacherTruth",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 10, 26, 13, 6, 21, 468, DateTimeKind.Local).AddTicks(5692));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 10, 26, 13, 6, 21, 476, DateTimeKind.Local).AddTicks(8052));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 10, 26, 13, 6, 21, 476, DateTimeKind.Local).AddTicks(8083));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 10, 26, 13, 6, 21, 476, DateTimeKind.Local).AddTicks(8092));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 10, 26, 13, 6, 21, 484, DateTimeKind.Local).AddTicks(6376));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 10, 26, 13, 6, 21, 484, DateTimeKind.Local).AddTicks(8430));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 10, 26, 13, 6, 21, 484, DateTimeKind.Local).AddTicks(8474));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 10, 26, 13, 6, 21, 484, DateTimeKind.Local).AddTicks(8490));
        }
    }
}
