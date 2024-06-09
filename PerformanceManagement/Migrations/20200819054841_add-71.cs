using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add71 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoacherDepartmentId",
                table: "Addressee",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CoacherDepartmnetEffectiveStartDate",
                table: "Addressee",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CoacherEffectiveStartDate",
                table: "Addressee",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoacherId",
                table: "Addressee",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 19, 10, 18, 40, 353, DateTimeKind.Local).AddTicks(6946));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 19, 10, 18, 40, 356, DateTimeKind.Local).AddTicks(5913));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 19, 10, 18, 40, 356, DateTimeKind.Local).AddTicks(5934));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 19, 10, 18, 40, 356, DateTimeKind.Local).AddTicks(5938));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 19, 10, 18, 40, 364, DateTimeKind.Local).AddTicks(8870));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 19, 10, 18, 40, 365, DateTimeKind.Local).AddTicks(83));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 19, 10, 18, 40, 365, DateTimeKind.Local).AddTicks(118));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 19, 10, 18, 40, 365, DateTimeKind.Local).AddTicks(242));

            migrationBuilder.CreateIndex(
                name: "IX_Addressee_CoacherDepartmentId_CoacherDepartmnetEffectiveStartDate",
                table: "Addressee",
                columns: new[] { "CoacherDepartmentId", "CoacherDepartmnetEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Addressee_CoacherId_CoacherEffectiveStartDate",
                table: "Addressee",
                columns: new[] { "CoacherId", "CoacherEffectiveStartDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_Addressee_evaluationHierarchies_CoacherDepartmentId_CoacherDepartmnetEffectiveStartDate",
                table: "Addressee",
                columns: new[] { "CoacherDepartmentId", "CoacherDepartmnetEffectiveStartDate" },
                principalTable: "evaluationHierarchies",
                principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Addressee_People_CoacherId_CoacherEffectiveStartDate",
                table: "Addressee",
                columns: new[] { "CoacherId", "CoacherEffectiveStartDate" },
                principalTable: "People",
                principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addressee_evaluationHierarchies_CoacherDepartmentId_CoacherDepartmnetEffectiveStartDate",
                table: "Addressee");

            migrationBuilder.DropForeignKey(
                name: "FK_Addressee_People_CoacherId_CoacherEffectiveStartDate",
                table: "Addressee");

            migrationBuilder.DropIndex(
                name: "IX_Addressee_CoacherDepartmentId_CoacherDepartmnetEffectiveStartDate",
                table: "Addressee");

            migrationBuilder.DropIndex(
                name: "IX_Addressee_CoacherId_CoacherEffectiveStartDate",
                table: "Addressee");

            migrationBuilder.DropColumn(
                name: "CoacherDepartmentId",
                table: "Addressee");

            migrationBuilder.DropColumn(
                name: "CoacherDepartmnetEffectiveStartDate",
                table: "Addressee");

            migrationBuilder.DropColumn(
                name: "CoacherEffectiveStartDate",
                table: "Addressee");

            migrationBuilder.DropColumn(
                name: "CoacherId",
                table: "Addressee");

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 18, 13, 11, 4, 434, DateTimeKind.Local).AddTicks(8140));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 18, 13, 11, 4, 437, DateTimeKind.Local).AddTicks(3569));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 18, 13, 11, 4, 437, DateTimeKind.Local).AddTicks(3601));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 18, 13, 11, 4, 437, DateTimeKind.Local).AddTicks(3611));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 18, 13, 11, 4, 442, DateTimeKind.Local).AddTicks(9901));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 18, 13, 11, 4, 443, DateTimeKind.Local).AddTicks(636));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 18, 13, 11, 4, 443, DateTimeKind.Local).AddTicks(651));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 18, 13, 11, 4, 443, DateTimeKind.Local).AddTicks(655));
        }
    }
}
