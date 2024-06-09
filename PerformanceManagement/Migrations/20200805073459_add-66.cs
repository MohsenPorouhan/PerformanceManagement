using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add66 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExtendSectionPeriod",
                columns: table => new
                {
                    ExtendSectionPeriodId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateFrom = table.Column<DateTime>(nullable: true),
                    DateTo = table.Column<DateTime>(nullable: true),
                    SectionPeriodId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtendSectionPeriod", x => x.ExtendSectionPeriodId);
                    table.ForeignKey(
                        name: "FK_ExtendSectionPeriod_SectionPeriod_SectionPeriodId",
                        column: x => x.SectionPeriodId,
                        principalTable: "SectionPeriod",
                        principalColumn: "SectionPeriodId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExtendSectionPeriodWithPeople",
                columns: table => new
                {
                    ExtendSectionPeriodWithPeopleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExtendSectionPeriodId = table.Column<int>(nullable: false),
                    PeopleId = table.Column<int>(nullable: false),
                    PeopleEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    EvaluationHierarchyId = table.Column<int>(nullable: false),
                    EvaluationHierarchyEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtendSectionPeriodWithPeople", x => x.ExtendSectionPeriodWithPeopleId);
                    table.ForeignKey(
                        name: "FK_ExtendSectionPeriodWithPeople_ExtendSectionPeriod_ExtendSectionPeriodId",
                        column: x => x.ExtendSectionPeriodId,
                        principalTable: "ExtendSectionPeriod",
                        principalColumn: "ExtendSectionPeriodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExtendSectionPeriodWithPeople_evaluationHierarchies_EvaluationHierarchyId_EvaluationHierarchyEffectiveStartDate",
                        columns: x => new { x.EvaluationHierarchyId, x.EvaluationHierarchyEffectiveStartDate },
                        principalTable: "evaluationHierarchies",
                        principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExtendSectionPeriodWithPeople_People_PeopleId_PeopleEffectiveStartDate",
                        columns: x => new { x.PeopleId, x.PeopleEffectiveStartDate },
                        principalTable: "People",
                        principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ExtendSectionPeriod_SectionPeriodId",
                table: "ExtendSectionPeriod",
                column: "SectionPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtendSectionPeriodWithPeople_ExtendSectionPeriodId",
                table: "ExtendSectionPeriodWithPeople",
                column: "ExtendSectionPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtendSectionPeriodWithPeople_EvaluationHierarchyId_EvaluationHierarchyEffectiveStartDate",
                table: "ExtendSectionPeriodWithPeople",
                columns: new[] { "EvaluationHierarchyId", "EvaluationHierarchyEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_ExtendSectionPeriodWithPeople_PeopleId_PeopleEffectiveStartDate",
                table: "ExtendSectionPeriodWithPeople",
                columns: new[] { "PeopleId", "PeopleEffectiveStartDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtendSectionPeriodWithPeople");

            migrationBuilder.DropTable(
                name: "ExtendSectionPeriod");

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 29, 8, 30, 22, 555, DateTimeKind.Local).AddTicks(9224));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 29, 8, 30, 22, 560, DateTimeKind.Local).AddTicks(6800));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 29, 8, 30, 22, 560, DateTimeKind.Local).AddTicks(6849));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 29, 8, 30, 22, 560, DateTimeKind.Local).AddTicks(6860));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 29, 8, 30, 22, 570, DateTimeKind.Local).AddTicks(5023));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 29, 8, 30, 22, 570, DateTimeKind.Local).AddTicks(6811));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 29, 8, 30, 22, 570, DateTimeKind.Local).AddTicks(6847));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 29, 8, 30, 22, 570, DateTimeKind.Local).AddTicks(6860));
        }
    }
}
