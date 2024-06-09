using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add78 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoacherTruth_BehaviouralCompetency_BehaviouralCompetencyId",
                table: "CoacherTruth");

            migrationBuilder.DropForeignKey(
                name: "FK_CoacherTruth_PeriodDefinitoion_PeriodDefinitionId",
                table: "CoacherTruth");

            migrationBuilder.DropForeignKey(
                name: "FK_CoacherTruth_evaluationHierarchies_CoacherDepartmentId_CoacherDepartmenEffectiveStartDate",
                table: "CoacherTruth");

            migrationBuilder.DropForeignKey(
                name: "FK_CoacherTruth_People_CoacherId_CoacherEffectiveStartDate",
                table: "CoacherTruth");

            migrationBuilder.DropForeignKey(
                name: "FK_CoacherTruth_evaluationHierarchies_EmployeeDepartmentId_EmployeeDepartmenEffectiveStartDate",
                table: "CoacherTruth");

            migrationBuilder.DropForeignKey(
                name: "FK_CoacherTruth_People_EmployeeId_EmployeeEffectiveStartDate",
                table: "CoacherTruth");

            migrationBuilder.DropIndex(
                name: "IX_CoacherTruth_BehaviouralCompetencyId",
                table: "CoacherTruth");

            migrationBuilder.DropIndex(
                name: "IX_CoacherTruth_CoacherDepartmentId_CoacherDepartmenEffectiveStartDate",
                table: "CoacherTruth");

            migrationBuilder.DropIndex(
                name: "IX_CoacherTruth_CoacherId_CoacherEffectiveStartDate",
                table: "CoacherTruth");

            migrationBuilder.DropIndex(
                name: "IX_CoacherTruth_EmployeeDepartmentId_EmployeeDepartmenEffectiveStartDate",
                table: "CoacherTruth");

            migrationBuilder.DropIndex(
                name: "IX_CoacherTruth_EmployeeId_EmployeeEffectiveStartDate",
                table: "CoacherTruth");

            migrationBuilder.DropColumn(
                name: "BehaviouralCompetencyId",
                table: "CoacherTruth");

            migrationBuilder.DropColumn(
                name: "CoacherDepartmenEffectiveStartDate",
                table: "CoacherTruth");

            migrationBuilder.DropColumn(
                name: "CoacherDepartmentId",
                table: "CoacherTruth");

            migrationBuilder.DropColumn(
                name: "CoacherEffectiveStartDate",
                table: "CoacherTruth");

            migrationBuilder.DropColumn(
                name: "CoacherId",
                table: "CoacherTruth");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartmenEffectiveStartDate",
                table: "CoacherTruth");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartmentId",
                table: "CoacherTruth");

            migrationBuilder.DropColumn(
                name: "EmployeeEffectiveStartDate",
                table: "CoacherTruth");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "CoacherTruth");

            migrationBuilder.RenameColumn(
                name: "PeriodDefinitionId",
                table: "CoacherTruth",
                newName: "EvaluationBehaviouralCompetencyId");

            migrationBuilder.RenameIndex(
                name: "IX_CoacherTruth_PeriodDefinitionId",
                table: "CoacherTruth",
                newName: "IX_CoacherTruth_EvaluationBehaviouralCompetencyId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_CoacherTruth_EvaluationBehaviouralCompetency_EvaluationBehaviouralCompetencyId",
                table: "CoacherTruth",
                column: "EvaluationBehaviouralCompetencyId",
                principalTable: "EvaluationBehaviouralCompetency",
                principalColumn: "EvaluationBehaviouralCompetencyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoacherTruth_EvaluationBehaviouralCompetency_EvaluationBehaviouralCompetencyId",
                table: "CoacherTruth");

            migrationBuilder.RenameColumn(
                name: "EvaluationBehaviouralCompetencyId",
                table: "CoacherTruth",
                newName: "PeriodDefinitionId");

            migrationBuilder.RenameIndex(
                name: "IX_CoacherTruth_EvaluationBehaviouralCompetencyId",
                table: "CoacherTruth",
                newName: "IX_CoacherTruth_PeriodDefinitionId");

            migrationBuilder.AddColumn<int>(
                name: "BehaviouralCompetencyId",
                table: "CoacherTruth",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CoacherDepartmenEffectiveStartDate",
                table: "CoacherTruth",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoacherDepartmentId",
                table: "CoacherTruth",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CoacherEffectiveStartDate",
                table: "CoacherTruth",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoacherId",
                table: "CoacherTruth",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmployeeDepartmenEffectiveStartDate",
                table: "CoacherTruth",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeDepartmentId",
                table: "CoacherTruth",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmployeeEffectiveStartDate",
                table: "CoacherTruth",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "CoacherTruth",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 11, 4, 10, 47, 45, 417, DateTimeKind.Local).AddTicks(5987));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 11, 4, 10, 47, 45, 422, DateTimeKind.Local).AddTicks(4585));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 11, 4, 10, 47, 45, 422, DateTimeKind.Local).AddTicks(4603));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 11, 4, 10, 47, 45, 422, DateTimeKind.Local).AddTicks(4607));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 11, 4, 10, 47, 45, 429, DateTimeKind.Local).AddTicks(753));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 11, 4, 10, 47, 45, 429, DateTimeKind.Local).AddTicks(2117));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 11, 4, 10, 47, 45, 429, DateTimeKind.Local).AddTicks(2146));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 11, 4, 10, 47, 45, 429, DateTimeKind.Local).AddTicks(2154));

            migrationBuilder.CreateIndex(
                name: "IX_CoacherTruth_BehaviouralCompetencyId",
                table: "CoacherTruth",
                column: "BehaviouralCompetencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CoacherTruth_CoacherDepartmentId_CoacherDepartmenEffectiveStartDate",
                table: "CoacherTruth",
                columns: new[] { "CoacherDepartmentId", "CoacherDepartmenEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_CoacherTruth_CoacherId_CoacherEffectiveStartDate",
                table: "CoacherTruth",
                columns: new[] { "CoacherId", "CoacherEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_CoacherTruth_EmployeeDepartmentId_EmployeeDepartmenEffectiveStartDate",
                table: "CoacherTruth",
                columns: new[] { "EmployeeDepartmentId", "EmployeeDepartmenEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_CoacherTruth_EmployeeId_EmployeeEffectiveStartDate",
                table: "CoacherTruth",
                columns: new[] { "EmployeeId", "EmployeeEffectiveStartDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_CoacherTruth_BehaviouralCompetency_BehaviouralCompetencyId",
                table: "CoacherTruth",
                column: "BehaviouralCompetencyId",
                principalTable: "BehaviouralCompetency",
                principalColumn: "BehaviouralCompetencyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoacherTruth_PeriodDefinitoion_PeriodDefinitionId",
                table: "CoacherTruth",
                column: "PeriodDefinitionId",
                principalTable: "PeriodDefinitoion",
                principalColumn: "PeriodDefinitoionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoacherTruth_evaluationHierarchies_CoacherDepartmentId_CoacherDepartmenEffectiveStartDate",
                table: "CoacherTruth",
                columns: new[] { "CoacherDepartmentId", "CoacherDepartmenEffectiveStartDate" },
                principalTable: "evaluationHierarchies",
                principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoacherTruth_People_CoacherId_CoacherEffectiveStartDate",
                table: "CoacherTruth",
                columns: new[] { "CoacherId", "CoacherEffectiveStartDate" },
                principalTable: "People",
                principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoacherTruth_evaluationHierarchies_EmployeeDepartmentId_EmployeeDepartmenEffectiveStartDate",
                table: "CoacherTruth",
                columns: new[] { "EmployeeDepartmentId", "EmployeeDepartmenEffectiveStartDate" },
                principalTable: "evaluationHierarchies",
                principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoacherTruth_People_EmployeeId_EmployeeEffectiveStartDate",
                table: "CoacherTruth",
                columns: new[] { "EmployeeId", "EmployeeEffectiveStartDate" },
                principalTable: "People",
                principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
