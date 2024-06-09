using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add73 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProtestResponse_evaluationHierarchies_PersonDepartmentEvaluationHierarchyId_PersonDepartmentEffectiveStartDate",
                table: "ProtestResponse");

            migrationBuilder.DropForeignKey(
                name: "FK_ProtestResponse_People_PersonPeopleId_PersonEffectiveStartDate1",
                table: "ProtestResponse");

            migrationBuilder.DropIndex(
                name: "IX_ProtestResponse_PersonDepartmentEvaluationHierarchyId_PersonDepartmentEffectiveStartDate",
                table: "ProtestResponse");

            migrationBuilder.DropIndex(
                name: "IX_ProtestResponse_PersonPeopleId_PersonEffectiveStartDate1",
                table: "ProtestResponse");

            migrationBuilder.DropColumn(
                name: "PersonDepartmentEffectiveStartDate",
                table: "ProtestResponse");

            migrationBuilder.DropColumn(
                name: "PersonDepartmentEvaluationHierarchyId",
                table: "ProtestResponse");

            migrationBuilder.DropColumn(
                name: "PersonEffectiveStartDate1",
                table: "ProtestResponse");

            migrationBuilder.DropColumn(
                name: "PersonPeopleId",
                table: "ProtestResponse");

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 9, 6, 11, 0, 54, 161, DateTimeKind.Local).AddTicks(1002));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 9, 6, 11, 0, 54, 164, DateTimeKind.Local).AddTicks(6793));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 9, 6, 11, 0, 54, 164, DateTimeKind.Local).AddTicks(6825));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 9, 6, 11, 0, 54, 164, DateTimeKind.Local).AddTicks(6834));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 9, 6, 11, 0, 54, 174, DateTimeKind.Local).AddTicks(4498));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 9, 6, 11, 0, 54, 174, DateTimeKind.Local).AddTicks(5929));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 9, 6, 11, 0, 54, 174, DateTimeKind.Local).AddTicks(5961));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 9, 6, 11, 0, 54, 174, DateTimeKind.Local).AddTicks(5970));

            migrationBuilder.CreateIndex(
                name: "IX_ProtestResponse_PersonDepartmentId_PersonDepartmnetEffectiveStartDate",
                table: "ProtestResponse",
                columns: new[] { "PersonDepartmentId", "PersonDepartmnetEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_ProtestResponse_PersonId_PersonEffectiveStartDate",
                table: "ProtestResponse",
                columns: new[] { "PersonId", "PersonEffectiveStartDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProtestResponse_evaluationHierarchies_PersonDepartmentId_PersonDepartmnetEffectiveStartDate",
                table: "ProtestResponse",
                columns: new[] { "PersonDepartmentId", "PersonDepartmnetEffectiveStartDate" },
                principalTable: "evaluationHierarchies",
                principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProtestResponse_People_PersonId_PersonEffectiveStartDate",
                table: "ProtestResponse",
                columns: new[] { "PersonId", "PersonEffectiveStartDate" },
                principalTable: "People",
                principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProtestResponse_evaluationHierarchies_PersonDepartmentId_PersonDepartmnetEffectiveStartDate",
                table: "ProtestResponse");

            migrationBuilder.DropForeignKey(
                name: "FK_ProtestResponse_People_PersonId_PersonEffectiveStartDate",
                table: "ProtestResponse");

            migrationBuilder.DropIndex(
                name: "IX_ProtestResponse_PersonDepartmentId_PersonDepartmnetEffectiveStartDate",
                table: "ProtestResponse");

            migrationBuilder.DropIndex(
                name: "IX_ProtestResponse_PersonId_PersonEffectiveStartDate",
                table: "ProtestResponse");

            migrationBuilder.AddColumn<DateTime>(
                name: "PersonDepartmentEffectiveStartDate",
                table: "ProtestResponse",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonDepartmentEvaluationHierarchyId",
                table: "ProtestResponse",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PersonEffectiveStartDate1",
                table: "ProtestResponse",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonPeopleId",
                table: "ProtestResponse",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 19, 10, 49, 31, 697, DateTimeKind.Local).AddTicks(7629));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 19, 10, 49, 31, 700, DateTimeKind.Local).AddTicks(9226));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 19, 10, 49, 31, 700, DateTimeKind.Local).AddTicks(9254));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 19, 10, 49, 31, 700, DateTimeKind.Local).AddTicks(9263));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 19, 10, 49, 31, 707, DateTimeKind.Local).AddTicks(9317));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 19, 10, 49, 31, 708, DateTimeKind.Local).AddTicks(490));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 19, 10, 49, 31, 708, DateTimeKind.Local).AddTicks(515));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 19, 10, 49, 31, 708, DateTimeKind.Local).AddTicks(523));

            migrationBuilder.CreateIndex(
                name: "IX_ProtestResponse_PersonDepartmentEvaluationHierarchyId_PersonDepartmentEffectiveStartDate",
                table: "ProtestResponse",
                columns: new[] { "PersonDepartmentEvaluationHierarchyId", "PersonDepartmentEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_ProtestResponse_PersonPeopleId_PersonEffectiveStartDate1",
                table: "ProtestResponse",
                columns: new[] { "PersonPeopleId", "PersonEffectiveStartDate1" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProtestResponse_evaluationHierarchies_PersonDepartmentEvaluationHierarchyId_PersonDepartmentEffectiveStartDate",
                table: "ProtestResponse",
                columns: new[] { "PersonDepartmentEvaluationHierarchyId", "PersonDepartmentEffectiveStartDate" },
                principalTable: "evaluationHierarchies",
                principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProtestResponse_People_PersonPeopleId_PersonEffectiveStartDate1",
                table: "ProtestResponse",
                columns: new[] { "PersonPeopleId", "PersonEffectiveStartDate1" },
                principalTable: "People",
                principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
