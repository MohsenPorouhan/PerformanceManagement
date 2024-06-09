using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add46 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 20, 18, 49, 30, 116, DateTimeKind.Local).AddTicks(3195));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 20, 18, 49, 30, 118, DateTimeKind.Local).AddTicks(3938));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 20, 18, 49, 30, 118, DateTimeKind.Local).AddTicks(3956));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 20, 18, 49, 30, 118, DateTimeKind.Local).AddTicks(3959));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 20, 18, 49, 30, 123, DateTimeKind.Local).AddTicks(9457));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 20, 18, 49, 30, 123, DateTimeKind.Local).AddTicks(9894));

            migrationBuilder.InsertData(
                table: "NotificationTitle",
                columns: new[] { "NotificationTitleId", "CreatedDate", "Title" },
                values: new object[,]
                {
                    { 3, new DateTime(2020, 5, 20, 18, 49, 30, 123, DateTimeKind.Local).AddTicks(9907), "تعیین وضعیت سایرارزیاب وظایف" },
                    { 4, new DateTime(2020, 5, 20, 18, 49, 30, 123, DateTimeKind.Local).AddTicks(9912), "تعیین وضعیت سایر ارزیاب شایستگی رفتاری" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4);

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
                column: "CreatedDate",
                value: new DateTime(2020, 5, 18, 15, 7, 8, 233, DateTimeKind.Local).AddTicks(8646));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 18, 15, 7, 8, 233, DateTimeKind.Local).AddTicks(9129));
        }
    }
}
