using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add62 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EvaluationId",
                table: "RelatedTaskWithSensibleEvent",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EvaluationBehaviouralCompetencyId",
                table: "RelatedCompetencyWithSensibleEvent",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 28, 8, 43, 23, 797, DateTimeKind.Local).AddTicks(7525));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 28, 8, 43, 23, 801, DateTimeKind.Local).AddTicks(9769));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 28, 8, 43, 23, 801, DateTimeKind.Local).AddTicks(9808));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 28, 8, 43, 23, 801, DateTimeKind.Local).AddTicks(9815));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 28, 8, 43, 23, 807, DateTimeKind.Local).AddTicks(4979));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 28, 8, 43, 23, 807, DateTimeKind.Local).AddTicks(5928));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 28, 8, 43, 23, 807, DateTimeKind.Local).AddTicks(5948));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 7, 28, 8, 43, 23, 807, DateTimeKind.Local).AddTicks(5953));

            migrationBuilder.CreateIndex(
                name: "IX_RelatedTaskWithSensibleEvent_EvaluationId",
                table: "RelatedTaskWithSensibleEvent",
                column: "EvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedCompetencyWithSensibleEvent_EvaluationBehaviouralCompetencyId",
                table: "RelatedCompetencyWithSensibleEvent",
                column: "EvaluationBehaviouralCompetencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedCompetencyWithSensibleEvent_EvaluationBehaviouralCompetency_EvaluationBehaviouralCompetencyId",
                table: "RelatedCompetencyWithSensibleEvent",
                column: "EvaluationBehaviouralCompetencyId",
                principalTable: "EvaluationBehaviouralCompetency",
                principalColumn: "EvaluationBehaviouralCompetencyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedTaskWithSensibleEvent_Evaluation_EvaluationId",
                table: "RelatedTaskWithSensibleEvent",
                column: "EvaluationId",
                principalTable: "Evaluation",
                principalColumn: "EvaluationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatedCompetencyWithSensibleEvent_EvaluationBehaviouralCompetency_EvaluationBehaviouralCompetencyId",
                table: "RelatedCompetencyWithSensibleEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedTaskWithSensibleEvent_Evaluation_EvaluationId",
                table: "RelatedTaskWithSensibleEvent");

            migrationBuilder.DropIndex(
                name: "IX_RelatedTaskWithSensibleEvent_EvaluationId",
                table: "RelatedTaskWithSensibleEvent");

            migrationBuilder.DropIndex(
                name: "IX_RelatedCompetencyWithSensibleEvent_EvaluationBehaviouralCompetencyId",
                table: "RelatedCompetencyWithSensibleEvent");

            migrationBuilder.DropColumn(
                name: "EvaluationId",
                table: "RelatedTaskWithSensibleEvent");

            migrationBuilder.DropColumn(
                name: "EvaluationBehaviouralCompetencyId",
                table: "RelatedCompetencyWithSensibleEvent");

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
        }
    }
}
