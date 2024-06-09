using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add43 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EvaluationAcceptanceStatusId",
                table: "EvaluationBehaviouralCompetency",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefutationCause",
                table: "EvaluationBehaviouralCompetency",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 18, 13, 45, 59, 327, DateTimeKind.Local).AddTicks(7237));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 18, 13, 45, 59, 329, DateTimeKind.Local).AddTicks(7085));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 18, 13, 45, 59, 329, DateTimeKind.Local).AddTicks(7101));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 18, 13, 45, 59, 329, DateTimeKind.Local).AddTicks(7105));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 18, 13, 45, 59, 334, DateTimeKind.Local).AddTicks(7125));

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationBehaviouralCompetency_EvaluationAcceptanceStatusId",
                table: "EvaluationBehaviouralCompetency",
                column: "EvaluationAcceptanceStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationBehaviouralCompetency_EvaluationAcceptanceStatus_EvaluationAcceptanceStatusId",
                table: "EvaluationBehaviouralCompetency",
                column: "EvaluationAcceptanceStatusId",
                principalTable: "EvaluationAcceptanceStatus",
                principalColumn: "EvaluationAcceptanceStatusId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationBehaviouralCompetency_EvaluationAcceptanceStatus_EvaluationAcceptanceStatusId",
                table: "EvaluationBehaviouralCompetency");

            migrationBuilder.DropIndex(
                name: "IX_EvaluationBehaviouralCompetency_EvaluationAcceptanceStatusId",
                table: "EvaluationBehaviouralCompetency");

            migrationBuilder.DropColumn(
                name: "EvaluationAcceptanceStatusId",
                table: "EvaluationBehaviouralCompetency");

            migrationBuilder.DropColumn(
                name: "RefutationCause",
                table: "EvaluationBehaviouralCompetency");

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 17, 16, 21, 57, 810, DateTimeKind.Local).AddTicks(6369));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 17, 16, 21, 57, 812, DateTimeKind.Local).AddTicks(656));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 17, 16, 21, 57, 812, DateTimeKind.Local).AddTicks(667));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 17, 16, 21, 57, 812, DateTimeKind.Local).AddTicks(670));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 17, 16, 21, 57, 814, DateTimeKind.Local).AddTicks(7734));
        }
    }
}
