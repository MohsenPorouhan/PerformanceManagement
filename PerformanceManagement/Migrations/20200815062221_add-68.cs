using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add68 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MultipleCoacherOfEmployeeFinalCalc");

            migrationBuilder.CreateTable(
                name: "MultipleCompetencyCoacherOfEmployeeFinalCalc",
                columns: table => new
                {
                    MultipleCompetencyCoacherOfEmployeeFinalCalcId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    EmployeeEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    PeriodDefinitionId = table.Column<int>(nullable: false),
                    FinalCompetencyScore = table.Column<int>(nullable: false),
                    ApplyCompetencyPercent = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleCompetencyCoacherOfEmployeeFinalCalc", x => x.MultipleCompetencyCoacherOfEmployeeFinalCalcId);
                    table.ForeignKey(
                        name: "FK_MultipleCompetencyCoacherOfEmployeeFinalCalc_PeriodDefinitoion_PeriodDefinitionId",
                        column: x => x.PeriodDefinitionId,
                        principalTable: "PeriodDefinitoion",
                        principalColumn: "PeriodDefinitoionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MultipleCompetencyCoacherOfEmployeeFinalCalc_People_EmployeeId_EmployeeEffectiveStartDate",
                        columns: x => new { x.EmployeeId, x.EmployeeEffectiveStartDate },
                        principalTable: "People",
                        principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MultipleTaskCoacherOfEmployeeFinalCalc",
                columns: table => new
                {
                    MultipleTaskCoacherOfEmployeeFinalCalcId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    EmployeeEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    PeriodDefinitionId = table.Column<int>(nullable: false),
                    FinalTaskScore = table.Column<int>(nullable: false),
                    ApplyTaskPercent = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleTaskCoacherOfEmployeeFinalCalc", x => x.MultipleTaskCoacherOfEmployeeFinalCalcId);
                    table.ForeignKey(
                        name: "FK_MultipleTaskCoacherOfEmployeeFinalCalc_PeriodDefinitoion_PeriodDefinitionId",
                        column: x => x.PeriodDefinitionId,
                        principalTable: "PeriodDefinitoion",
                        principalColumn: "PeriodDefinitoionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MultipleTaskCoacherOfEmployeeFinalCalc_People_EmployeeId_EmployeeEffectiveStartDate",
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
                value: new DateTime(2020, 8, 15, 10, 52, 20, 823, DateTimeKind.Local).AddTicks(7351));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 10, 52, 20, 827, DateTimeKind.Local).AddTicks(7755));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 10, 52, 20, 827, DateTimeKind.Local).AddTicks(7775));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 10, 52, 20, 827, DateTimeKind.Local).AddTicks(7780));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 10, 52, 20, 833, DateTimeKind.Local).AddTicks(6674));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 10, 52, 20, 833, DateTimeKind.Local).AddTicks(7780));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 10, 52, 20, 833, DateTimeKind.Local).AddTicks(7812));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 10, 52, 20, 833, DateTimeKind.Local).AddTicks(7818));

            migrationBuilder.CreateIndex(
                name: "IX_MultipleCompetencyCoacherOfEmployeeFinalCalc_PeriodDefinitionId",
                table: "MultipleCompetencyCoacherOfEmployeeFinalCalc",
                column: "PeriodDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_MultipleCompetencyCoacherOfEmployeeFinalCalc_EmployeeId_EmployeeEffectiveStartDate",
                table: "MultipleCompetencyCoacherOfEmployeeFinalCalc",
                columns: new[] { "EmployeeId", "EmployeeEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_MultipleTaskCoacherOfEmployeeFinalCalc_PeriodDefinitionId",
                table: "MultipleTaskCoacherOfEmployeeFinalCalc",
                column: "PeriodDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_MultipleTaskCoacherOfEmployeeFinalCalc_EmployeeId_EmployeeEffectiveStartDate",
                table: "MultipleTaskCoacherOfEmployeeFinalCalc",
                columns: new[] { "EmployeeId", "EmployeeEffectiveStartDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MultipleCompetencyCoacherOfEmployeeFinalCalc");

            migrationBuilder.DropTable(
                name: "MultipleTaskCoacherOfEmployeeFinalCalc");

            migrationBuilder.CreateTable(
                name: "MultipleCoacherOfEmployeeFinalCalc",
                columns: table => new
                {
                    MultipleCoacherOfEmployeeFinalCalcId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplyCompetencyTask = table.Column<int>(nullable: true),
                    ApplyTaskPercent = table.Column<int>(nullable: true),
                    EmployeeEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false),
                    FinalCompetencyScore = table.Column<int>(nullable: true),
                    FinalTaskScore = table.Column<int>(nullable: true),
                    PeriodDefinitionId = table.Column<int>(nullable: false)
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
    }
}
