using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add59 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SensibleEvent",
                columns: table => new
                {
                    SensibleEventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    EventType = table.Column<int>(nullable: false),
                    SensibleEventDate = table.Column<DateTime>(nullable: false),
                    Visibility = table.Column<bool>(nullable: false),
                    ImportantWeight = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    PersonEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    PersonDepartmentId = table.Column<int>(nullable: false),
                    DepartmentEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    PersonRole = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensibleEvent", x => x.SensibleEventId);
                    table.ForeignKey(
                        name: "FK_SensibleEvent_evaluationHierarchies_PersonDepartmentId_DepartmentEffectiveStartDate",
                        columns: x => new { x.PersonDepartmentId, x.DepartmentEffectiveStartDate },
                        principalTable: "evaluationHierarchies",
                        principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SensibleEvent_People_PersonId_PersonEffectiveStartDate",
                        columns: x => new { x.PersonId, x.PersonEffectiveStartDate },
                        principalTable: "People",
                        principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RelatedCompetencyWithSensibleEvent",
                columns: table => new
                {
                    RelatedCompetencyWithSensibleEventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SensibleEventId = table.Column<int>(nullable: false),
                    BehaviouralCompetencyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedCompetencyWithSensibleEvent", x => x.RelatedCompetencyWithSensibleEventId);
                    table.ForeignKey(
                        name: "FK_RelatedCompetencyWithSensibleEvent_BehaviouralCompetency_BehaviouralCompetencyId",
                        column: x => x.BehaviouralCompetencyId,
                        principalTable: "BehaviouralCompetency",
                        principalColumn: "BehaviouralCompetencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelatedCompetencyWithSensibleEvent_SensibleEvent_SensibleEventId",
                        column: x => x.SensibleEventId,
                        principalTable: "SensibleEvent",
                        principalColumn: "SensibleEventId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RelatedPeopleWithSensibleEvent",
                columns: table => new
                {
                    RelatedPeopleWithSensibleEventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SensibleEventId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    EmployeeEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: false),
                    DepartmentEffectiveStartDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedPeopleWithSensibleEvent", x => x.RelatedPeopleWithSensibleEventId);
                    table.ForeignKey(
                        name: "FK_RelatedPeopleWithSensibleEvent_SensibleEvent_SensibleEventId",
                        column: x => x.SensibleEventId,
                        principalTable: "SensibleEvent",
                        principalColumn: "SensibleEventId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelatedPeopleWithSensibleEvent_evaluationHierarchies_DepartmentId_DepartmentEffectiveStartDate",
                        columns: x => new { x.DepartmentId, x.DepartmentEffectiveStartDate },
                        principalTable: "evaluationHierarchies",
                        principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelatedPeopleWithSensibleEvent_People_EmployeeId_EmployeeEffectiveStartDate",
                        columns: x => new { x.EmployeeId, x.EmployeeEffectiveStartDate },
                        principalTable: "People",
                        principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RelatedTaskWithSensibleEvent",
                columns: table => new
                {
                    RelatedTaskWithSensibleEventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SensibleEventId = table.Column<int>(nullable: false),
                    TaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedTaskWithSensibleEvent", x => x.RelatedTaskWithSensibleEventId);
                    table.ForeignKey(
                        name: "FK_RelatedTaskWithSensibleEvent_SensibleEvent_SensibleEventId",
                        column: x => x.SensibleEventId,
                        principalTable: "SensibleEvent",
                        principalColumn: "SensibleEventId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelatedTaskWithSensibleEvent_Task_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 25, 14, 4, 26, 584, DateTimeKind.Local).AddTicks(3055));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 25, 14, 4, 26, 588, DateTimeKind.Local).AddTicks(9848));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 25, 14, 4, 26, 588, DateTimeKind.Local).AddTicks(9879));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 25, 14, 4, 26, 588, DateTimeKind.Local).AddTicks(9888));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 25, 14, 4, 26, 599, DateTimeKind.Local).AddTicks(4514));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 25, 14, 4, 26, 599, DateTimeKind.Local).AddTicks(6034));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 25, 14, 4, 26, 599, DateTimeKind.Local).AddTicks(6063));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 25, 14, 4, 26, 599, DateTimeKind.Local).AddTicks(6071));

            migrationBuilder.CreateIndex(
                name: "IX_RelatedCompetencyWithSensibleEvent_BehaviouralCompetencyId",
                table: "RelatedCompetencyWithSensibleEvent",
                column: "BehaviouralCompetencyId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedCompetencyWithSensibleEvent_SensibleEventId",
                table: "RelatedCompetencyWithSensibleEvent",
                column: "SensibleEventId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedPeopleWithSensibleEvent_SensibleEventId",
                table: "RelatedPeopleWithSensibleEvent",
                column: "SensibleEventId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedPeopleWithSensibleEvent_DepartmentId_DepartmentEffectiveStartDate",
                table: "RelatedPeopleWithSensibleEvent",
                columns: new[] { "DepartmentId", "DepartmentEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_RelatedPeopleWithSensibleEvent_EmployeeId_EmployeeEffectiveStartDate",
                table: "RelatedPeopleWithSensibleEvent",
                columns: new[] { "EmployeeId", "EmployeeEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_RelatedTaskWithSensibleEvent_SensibleEventId",
                table: "RelatedTaskWithSensibleEvent",
                column: "SensibleEventId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedTaskWithSensibleEvent_TaskId",
                table: "RelatedTaskWithSensibleEvent",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_SensibleEvent_PersonDepartmentId_DepartmentEffectiveStartDate",
                table: "SensibleEvent",
                columns: new[] { "PersonDepartmentId", "DepartmentEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_SensibleEvent_PersonId_PersonEffectiveStartDate",
                table: "SensibleEvent",
                columns: new[] { "PersonId", "PersonEffectiveStartDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelatedCompetencyWithSensibleEvent");

            migrationBuilder.DropTable(
                name: "RelatedPeopleWithSensibleEvent");

            migrationBuilder.DropTable(
                name: "RelatedTaskWithSensibleEvent");

            migrationBuilder.DropTable(
                name: "SensibleEvent");

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 15, 17, 46, 39, 184, DateTimeKind.Local).AddTicks(5801));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 15, 17, 46, 39, 186, DateTimeKind.Local).AddTicks(6513));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 15, 17, 46, 39, 186, DateTimeKind.Local).AddTicks(6532));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 15, 17, 46, 39, 186, DateTimeKind.Local).AddTicks(6537));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 15, 17, 46, 39, 191, DateTimeKind.Local).AddTicks(2663));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 15, 17, 46, 39, 191, DateTimeKind.Local).AddTicks(3457));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 15, 17, 46, 39, 191, DateTimeKind.Local).AddTicks(3474));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 15, 17, 46, 39, 191, DateTimeKind.Local).AddTicks(3479));
        }
    }
}
