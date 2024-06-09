using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add64 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeriodDefinitionId",
                table: "SensibleEvent",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 29, 8, 28, 12, 824, DateTimeKind.Local).AddTicks(2759));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 29, 8, 28, 12, 826, DateTimeKind.Local).AddTicks(8658));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 29, 8, 28, 12, 826, DateTimeKind.Local).AddTicks(8681));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 29, 8, 28, 12, 826, DateTimeKind.Local).AddTicks(8686));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 29, 8, 28, 12, 832, DateTimeKind.Local).AddTicks(5372));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 29, 8, 28, 12, 832, DateTimeKind.Local).AddTicks(6339));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 29, 8, 28, 12, 832, DateTimeKind.Local).AddTicks(6358));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 29, 8, 28, 12, 832, DateTimeKind.Local).AddTicks(6363));

            migrationBuilder.CreateIndex(
                name: "IX_SensibleEvent_PeriodDefinitionId",
                table: "SensibleEvent",
                column: "PeriodDefinitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SensibleEvent_PeriodDefinitoion_PeriodDefinitionId",
                table: "SensibleEvent",
                column: "PeriodDefinitionId",
                principalTable: "PeriodDefinitoion",
                principalColumn: "PeriodDefinitoionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SensibleEvent_PeriodDefinitoion_PeriodDefinitionId",
                table: "SensibleEvent");

            migrationBuilder.DropIndex(
                name: "IX_SensibleEvent_PeriodDefinitionId",
                table: "SensibleEvent");

            migrationBuilder.DropColumn(
                name: "PeriodDefinitionId",
                table: "SensibleEvent");

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 28, 9, 4, 11, 808, DateTimeKind.Local).AddTicks(8673));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 28, 9, 4, 11, 813, DateTimeKind.Local).AddTicks(1663));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 28, 9, 4, 11, 813, DateTimeKind.Local).AddTicks(1702));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 28, 9, 4, 11, 813, DateTimeKind.Local).AddTicks(1711));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 28, 9, 4, 11, 829, DateTimeKind.Local).AddTicks(7054));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 28, 9, 4, 11, 829, DateTimeKind.Local).AddTicks(8391));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 28, 9, 4, 11, 829, DateTimeKind.Local).AddTicks(8419));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 28, 9, 4, 11, 829, DateTimeKind.Local).AddTicks(8426));
        }
    }
}
