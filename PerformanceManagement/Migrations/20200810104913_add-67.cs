using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add67 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MultipleCoacherWeight",
                table: "FinalScoreCompetencyCalculation",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MultipleCoacherWeight",
                table: "FinalScoreCalculation",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MultipleCoacherOfEmployeeFinalCalc",
                columns: table => new
                {
                    MultipleCoacherOfEmployeeFinalCalcId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    EmployeeEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    PeriodDefinitionId = table.Column<int>(nullable: false),
                    FinalTaskScore = table.Column<int>(nullable: true),
                    FinalCompetencyScore = table.Column<int>(nullable: true),
                    ApplyTaskPercent = table.Column<int>(nullable: true),
                    ApplyCompetencyTask = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleCoacherOfEmployeeFinalCalc", x => x.MultipleCoacherOfEmployeeFinalCalcId);
                    table.ForeignKey(
                        name: "FK_MultipleCoacherOfEmployeeFinalCalc_PeriodDefinitoion_PeriodDefinitionId",
                        column: x => x.PeriodDefinitionId,
                        principalTable: "PeriodDefinitoion",
                        principalColumn: "PeriodDefinitoionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MultipleCoacherOfEmployeeFinalCalc_People_EmployeeId_EmployeeEffectiveStartDate",
                        columns: x => new { x.EmployeeId, x.EmployeeEffectiveStartDate },
                        principalTable: "People",
                        principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 10, 15, 19, 12, 262, DateTimeKind.Local).AddTicks(6235));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 10, 15, 19, 12, 266, DateTimeKind.Local).AddTicks(9940));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 10, 15, 19, 12, 266, DateTimeKind.Local).AddTicks(9960));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 10, 15, 19, 12, 266, DateTimeKind.Local).AddTicks(9963));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 10, 15, 19, 12, 272, DateTimeKind.Local).AddTicks(606));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 10, 15, 19, 12, 272, DateTimeKind.Local).AddTicks(1548));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 10, 15, 19, 12, 272, DateTimeKind.Local).AddTicks(1568));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 10, 15, 19, 12, 272, DateTimeKind.Local).AddTicks(1574));

            migrationBuilder.CreateIndex(
                name: "IX_MultipleCoacherOfEmployeeFinalCalc_PeriodDefinitionId",
                table: "MultipleCoacherOfEmployeeFinalCalc",
                column: "PeriodDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_MultipleCoacherOfEmployeeFinalCalc_EmployeeId_EmployeeEffectiveStartDate",
                table: "MultipleCoacherOfEmployeeFinalCalc",
                columns: new[] { "EmployeeId", "EmployeeEffectiveStartDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MultipleCoacherOfEmployeeFinalCalc");

            migrationBuilder.DropColumn(
                name: "MultipleCoacherWeight",
                table: "FinalScoreCompetencyCalculation");

            migrationBuilder.DropColumn(
                name: "MultipleCoacherWeight",
                table: "FinalScoreCalculation");

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 5, 12, 4, 58, 720, DateTimeKind.Local).AddTicks(162));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 5, 12, 4, 58, 722, DateTimeKind.Local).AddTicks(350));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 5, 12, 4, 58, 722, DateTimeKind.Local).AddTicks(373));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 5, 12, 4, 58, 722, DateTimeKind.Local).AddTicks(378));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 5, 12, 4, 58, 730, DateTimeKind.Local).AddTicks(4825));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 5, 12, 4, 58, 730, DateTimeKind.Local).AddTicks(5418));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 5, 12, 4, 58, 730, DateTimeKind.Local).AddTicks(5429));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 5, 12, 4, 58, 730, DateTimeKind.Local).AddTicks(5432));
        }
    }
}
