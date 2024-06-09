using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add45 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 18, 15, 7, 8, 223, DateTimeKind.Local).AddTicks(2989));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 18, 15, 7, 8, 225, DateTimeKind.Local).AddTicks(5138));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 18, 15, 7, 8, 225, DateTimeKind.Local).AddTicks(5157));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 18, 15, 7, 8, 225, DateTimeKind.Local).AddTicks(5162));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2020, 5, 18, 15, 7, 8, 233, DateTimeKind.Local).AddTicks(8646), "تغییر وضعیت تفاهم وظایف" });

            migrationBuilder.InsertData(
                table: "NotificationTitle",
                columns: new[] { "NotificationTitleId", "CreatedDate", "Title" },
                values: new object[] { 2, new DateTime(2020, 5, 18, 15, 7, 8, 233, DateTimeKind.Local).AddTicks(9129), "تغییر وضعیت تفاهم شایستگی ها" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 18, 13, 50, 26, 549, DateTimeKind.Local).AddTicks(4786));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 18, 13, 50, 26, 551, DateTimeKind.Local).AddTicks(6407));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 18, 13, 50, 26, 551, DateTimeKind.Local).AddTicks(6424));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 18, 13, 50, 26, 551, DateTimeKind.Local).AddTicks(6428));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2020, 5, 18, 13, 50, 26, 556, DateTimeKind.Local).AddTicks(9702), "تغییر وضعیت تفاهم" });
        }
    }
}
