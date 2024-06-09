using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add76 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2020, 10, 26, 13, 6, 21, 476, DateTimeKind.Local).AddTicks(8052), "ابلاغی" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 10, 19, 12, 52, 27, 485, DateTimeKind.Local).AddTicks(8558));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2020, 10, 19, 12, 52, 27, 490, DateTimeKind.Local).AddTicks(1824), "دستوری" });

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 10, 19, 12, 52, 27, 490, DateTimeKind.Local).AddTicks(1842));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 10, 19, 12, 52, 27, 490, DateTimeKind.Local).AddTicks(1846));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 10, 19, 12, 52, 27, 494, DateTimeKind.Local).AddTicks(6703));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 10, 19, 12, 52, 27, 494, DateTimeKind.Local).AddTicks(7481));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 10, 19, 12, 52, 27, 494, DateTimeKind.Local).AddTicks(7498));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 10, 19, 12, 52, 27, 494, DateTimeKind.Local).AddTicks(7502));
        }
    }
}
