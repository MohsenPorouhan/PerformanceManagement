using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add39 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationTitle",
                columns: table => new
                {
                    NotificationTitleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTitle", x => x.NotificationTitleId);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    NotificationTitleId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ReceiverPersonId = table.Column<int>(nullable: false),
                    ReceiverHierarchtId = table.Column<int>(nullable: true),
                    AllocatorPersonId = table.Column<int>(nullable: false),
                    AllocatorHierarchyId = table.Column<int>(nullable: true),
                    AllocatorRoleId = table.Column<string>(nullable: true),
                    ReceiverRoleId = table.Column<string>(nullable: true),
                    PeriodDefinitionId = table.Column<int>(nullable: true),
                    Visibility = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notification_NotificationTitle_NotificationTitleId",
                        column: x => x.NotificationTitleId,
                        principalTable: "NotificationTitle",
                        principalColumn: "NotificationTitleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 17, 13, 57, 2, 768, DateTimeKind.Local).AddTicks(5312));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 17, 13, 57, 2, 769, DateTimeKind.Local).AddTicks(9465));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 17, 13, 57, 2, 769, DateTimeKind.Local).AddTicks(9477));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 17, 13, 57, 2, 769, DateTimeKind.Local).AddTicks(9480));

            migrationBuilder.CreateIndex(
                name: "IX_Notification_NotificationTitleId",
                table: "Notification",
                column: "NotificationTitleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "NotificationTitle");

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 14, 14, 45, 17, 44, DateTimeKind.Local).AddTicks(1017));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 14, 14, 45, 17, 46, DateTimeKind.Local).AddTicks(197));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 14, 14, 45, 17, 46, DateTimeKind.Local).AddTicks(214));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 5, 14, 14, 45, 17, 46, DateTimeKind.Local).AddTicks(219));
        }
    }
}
