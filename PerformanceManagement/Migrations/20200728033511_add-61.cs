using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add61 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelatedPeopleWithSensibleEvent");

            migrationBuilder.RenameColumn(
                name: "ImportantWeight",
                table: "SensibleEvent",
                newName: "EmployeeId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SensibleEvent",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmployeeDepartmentEffectiveStartDate",
                table: "SensibleEvent",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeDepartmentId",
                table: "SensibleEvent",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmployeeEffectiveStartDate",
                table: "SensibleEvent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "SensibleEvent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "SensibleEvent",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 28, 8, 5, 11, 262, DateTimeKind.Local).AddTicks(9461));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 28, 8, 5, 11, 264, DateTimeKind.Local).AddTicks(7498));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 28, 8, 5, 11, 264, DateTimeKind.Local).AddTicks(7514));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 28, 8, 5, 11, 264, DateTimeKind.Local).AddTicks(7518));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 28, 8, 5, 11, 268, DateTimeKind.Local).AddTicks(9472));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 28, 8, 5, 11, 269, DateTimeKind.Local).AddTicks(134));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 28, 8, 5, 11, 269, DateTimeKind.Local).AddTicks(146));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 28, 8, 5, 11, 269, DateTimeKind.Local).AddTicks(150));

            migrationBuilder.CreateIndex(
                name: "IX_SensibleEvent_EmployeeDepartmentId_DepartmentEffectiveStartDate",
                table: "SensibleEvent",
                columns: new[] { "EmployeeDepartmentId", "DepartmentEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_SensibleEvent_EmployeeId_EmployeeEffectiveStartDate",
                table: "SensibleEvent",
                columns: new[] { "EmployeeId", "EmployeeEffectiveStartDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_SensibleEvent_evaluationHierarchies_EmployeeDepartmentId_DepartmentEffectiveStartDate",
                table: "SensibleEvent",
                columns: new[] { "EmployeeDepartmentId", "DepartmentEffectiveStartDate" },
                principalTable: "evaluationHierarchies",
                principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SensibleEvent_People_EmployeeId_EmployeeEffectiveStartDate",
                table: "SensibleEvent",
                columns: new[] { "EmployeeId", "EmployeeEffectiveStartDate" },
                principalTable: "People",
                principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SensibleEvent_evaluationHierarchies_EmployeeDepartmentId_DepartmentEffectiveStartDate",
                table: "SensibleEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_SensibleEvent_People_EmployeeId_EmployeeEffectiveStartDate",
                table: "SensibleEvent");

            migrationBuilder.DropIndex(
                name: "IX_SensibleEvent_EmployeeDepartmentId_DepartmentEffectiveStartDate",
                table: "SensibleEvent");

            migrationBuilder.DropIndex(
                name: "IX_SensibleEvent_EmployeeId_EmployeeEffectiveStartDate",
                table: "SensibleEvent");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "SensibleEvent");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartmentEffectiveStartDate",
                table: "SensibleEvent");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartmentId",
                table: "SensibleEvent");

            migrationBuilder.DropColumn(
                name: "EmployeeEffectiveStartDate",
                table: "SensibleEvent");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "SensibleEvent");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "SensibleEvent");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "SensibleEvent",
                newName: "ImportantWeight");

            migrationBuilder.CreateTable(
                name: "RelatedPeopleWithSensibleEvent",
                columns: table => new
                {
                    RelatedPeopleWithSensibleEventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DepartmentEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: false),
                    EmployeeEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false),
                    SensibleEventId = table.Column<int>(nullable: false)
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

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 26, 13, 27, 15, 688, DateTimeKind.Local).AddTicks(1999));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 26, 13, 27, 15, 690, DateTimeKind.Local).AddTicks(6192));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 26, 13, 27, 15, 690, DateTimeKind.Local).AddTicks(6212));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 26, 13, 27, 15, 690, DateTimeKind.Local).AddTicks(6215));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 26, 13, 27, 15, 694, DateTimeKind.Local).AddTicks(9178));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 26, 13, 27, 15, 694, DateTimeKind.Local).AddTicks(9904));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 26, 13, 27, 15, 694, DateTimeKind.Local).AddTicks(9918));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 26, 13, 27, 15, 694, DateTimeKind.Local).AddTicks(9922));

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
        }
    }
}
