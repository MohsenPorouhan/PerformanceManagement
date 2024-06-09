using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add79 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScoreScheduleType",
                columns: table => new
                {
                    ScoreScheduleTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreScheduleType", x => x.ScoreScheduleTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ScoreSchedule",
                columns: table => new
                {
                    ScoreScheduleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ScoreScheduleTypeId = table.Column<int>(nullable: false),
                    DateFrom = table.Column<DateTime>(nullable: false),
                    DateTo = table.Column<DateTime>(nullable: false),
                    PeriodDefinitionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreSchedule", x => x.ScoreScheduleId);
                    table.ForeignKey(
                        name: "FK_ScoreSchedule_PeriodDefinitoion_PeriodDefinitionId",
                        column: x => x.PeriodDefinitionId,
                        principalTable: "PeriodDefinitoion",
                        principalColumn: "PeriodDefinitoionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScoreSchedule_ScoreScheduleType_ScoreScheduleTypeId",
                        column: x => x.ScoreScheduleTypeId,
                        principalTable: "ScoreScheduleType",
                        principalColumn: "ScoreScheduleTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 8, 10, 50, 29, 702, DateTimeKind.Local).AddTicks(6953));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 8, 10, 50, 29, 708, DateTimeKind.Local).AddTicks(5913));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 8, 10, 50, 29, 708, DateTimeKind.Local).AddTicks(5929));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 8, 10, 50, 29, 708, DateTimeKind.Local).AddTicks(5933));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 8, 10, 50, 29, 714, DateTimeKind.Local).AddTicks(4862));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 8, 10, 50, 29, 714, DateTimeKind.Local).AddTicks(5769));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 8, 10, 50, 29, 714, DateTimeKind.Local).AddTicks(5787));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 12, 8, 10, 50, 29, 714, DateTimeKind.Local).AddTicks(5793));

            migrationBuilder.InsertData(
                table: "ScoreScheduleType",
                columns: new[] { "ScoreScheduleTypeId", "CreatedDate", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 12, 8, 10, 50, 29, 744, DateTimeKind.Local).AddTicks(1889), "خود ارزیابی" },
                    { 2, new DateTime(2020, 12, 8, 10, 50, 29, 744, DateTimeKind.Local).AddTicks(2553), "سایرارزیاب" },
                    { 3, new DateTime(2020, 12, 8, 10, 50, 29, 744, DateTimeKind.Local).AddTicks(2566), "مربی سطح 1 و بالاتر از سطح 2" },
                    { 4, new DateTime(2020, 12, 8, 10, 50, 29, 744, DateTimeKind.Local).AddTicks(2569), "مربی سطح 2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScoreSchedule_PeriodDefinitionId",
                table: "ScoreSchedule",
                column: "PeriodDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreSchedule_ScoreScheduleTypeId",
                table: "ScoreSchedule",
                column: "ScoreScheduleTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScoreSchedule");

            migrationBuilder.DropTable(
                name: "ScoreScheduleType");

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 11, 4, 11, 36, 56, 436, DateTimeKind.Local).AddTicks(6305));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 11, 4, 11, 36, 56, 445, DateTimeKind.Local).AddTicks(6789));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 11, 4, 11, 36, 56, 445, DateTimeKind.Local).AddTicks(6813));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 11, 4, 11, 36, 56, 445, DateTimeKind.Local).AddTicks(6819));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 11, 4, 11, 36, 56, 452, DateTimeKind.Local).AddTicks(8807));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 11, 4, 11, 36, 56, 452, DateTimeKind.Local).AddTicks(9772));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 11, 4, 11, 36, 56, 452, DateTimeKind.Local).AddTicks(9791));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 11, 4, 11, 36, 56, 452, DateTimeKind.Local).AddTicks(9795));
        }
    }
}
