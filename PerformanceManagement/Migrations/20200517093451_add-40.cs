using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add40 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 17, 14, 4, 51, 64, DateTimeKind.Local).AddTicks(5507));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 17, 14, 4, 51, 66, DateTimeKind.Local).AddTicks(940));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 17, 14, 4, 51, 66, DateTimeKind.Local).AddTicks(952));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 17, 14, 4, 51, 66, DateTimeKind.Local).AddTicks(955));

            migrationBuilder.InsertData(
                table: "NotificationTitle",
                columns: new[] { "NotificationTitleId", "CreatedDate", "Title" },
                values: new object[] { 1, new DateTime(2020, 5, 17, 14, 4, 51, 69, DateTimeKind.Local).AddTicks(8141), "تغییر وضعیت تفاهم" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 17, 13, 57, 2, 768, DateTimeKind.Local).AddTicks(5312));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 17, 13, 57, 2, 769, DateTimeKind.Local).AddTicks(9465));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 17, 13, 57, 2, 769, DateTimeKind.Local).AddTicks(9477));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 17, 13, 57, 2, 769, DateTimeKind.Local).AddTicks(9480));
        }
    }
}
