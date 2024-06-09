using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add56 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinalizeCalculation",
                columns: table => new
                {
                    FinalizeCalculationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CoacherEffecStartDate = table.Column<DateTime>(nullable: true),
                    CocherId = table.Column<int>(nullable: false),
                    PeriodDefinitoionId = table.Column<int>(nullable: false),
                    IsFinalization = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalizeCalculation", x => x.FinalizeCalculationId);
                    table.ForeignKey(
                        name: "FK_FinalizeCalculation_PeriodDefinitoion_PeriodDefinitoionId",
                        column: x => x.PeriodDefinitoionId,
                        principalTable: "PeriodDefinitoion",
                        principalColumn: "PeriodDefinitoionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinalizeCalculation_People_CocherId_CoacherEffecStartDate",
                        columns: x => new { x.CocherId, x.CoacherEffecStartDate },
                        principalTable: "People",
                        principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 15, 41, 37, 227, DateTimeKind.Local).AddTicks(389));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 15, 41, 37, 230, DateTimeKind.Local).AddTicks(9000));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 15, 41, 37, 230, DateTimeKind.Local).AddTicks(9020));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 15, 41, 37, 230, DateTimeKind.Local).AddTicks(9024));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 15, 41, 37, 235, DateTimeKind.Local).AddTicks(2598));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 15, 41, 37, 235, DateTimeKind.Local).AddTicks(3288));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 15, 41, 37, 235, DateTimeKind.Local).AddTicks(3303));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 15, 41, 37, 235, DateTimeKind.Local).AddTicks(3307));

            migrationBuilder.CreateIndex(
                name: "IX_FinalizeCalculation_PeriodDefinitoionId",
                table: "FinalizeCalculation",
                column: "PeriodDefinitoionId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalizeCalculation_CocherId_CoacherEffecStartDate",
                table: "FinalizeCalculation",
                columns: new[] { "CocherId", "CoacherEffecStartDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinalizeCalculation");

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 14, 15, 20, 47, DateTimeKind.Local).AddTicks(2532));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 14, 15, 20, 52, DateTimeKind.Local).AddTicks(2821));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 14, 15, 20, 52, DateTimeKind.Local).AddTicks(2853));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 14, 15, 20, 52, DateTimeKind.Local).AddTicks(2861));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 14, 15, 20, 58, DateTimeKind.Local).AddTicks(9624));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 14, 15, 20, 59, DateTimeKind.Local).AddTicks(730));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 14, 15, 20, 59, DateTimeKind.Local).AddTicks(753));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 13, 14, 15, 20, 59, DateTimeKind.Local).AddTicks(761));
        }
    }
}
