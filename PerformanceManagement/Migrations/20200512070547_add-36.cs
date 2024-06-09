using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add36 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EvaluationAcceptanceStatusId",
                table: "Evaluation",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EvaluationAcceptanceStatus",
                columns: table => new
                {
                    EvaluationAcceptanceStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationAcceptanceStatus", x => x.EvaluationAcceptanceStatusId);
                });

            migrationBuilder.InsertData(
                table: "EvaluationAcceptanceStatus",
                columns: new[] { "EvaluationAcceptanceStatusId", "CreatedDate", "LastUpdatedBy", "LastUpdatedDate", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 5, 12, 11, 35, 47, 141, DateTimeKind.Local).AddTicks(9597), null, null, "تفاهم" },
                    { 2, new DateTime(2020, 5, 12, 11, 35, 47, 144, DateTimeKind.Local).AddTicks(7680), null, null, "دستوری" },
                    { 3, new DateTime(2020, 5, 12, 11, 35, 47, 144, DateTimeKind.Local).AddTicks(7694), null, null, "صرف نظر" },
                    { 4, new DateTime(2020, 5, 12, 11, 35, 47, 144, DateTimeKind.Local).AddTicks(7697), null, null, "نامشخص" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluation_EvaluationAcceptanceStatusId",
                table: "Evaluation",
                column: "EvaluationAcceptanceStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluation_EvaluationAcceptanceStatus_EvaluationAcceptanceStatusId",
                table: "Evaluation",
                column: "EvaluationAcceptanceStatusId",
                principalTable: "EvaluationAcceptanceStatus",
                principalColumn: "EvaluationAcceptanceStatusId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluation_EvaluationAcceptanceStatus_EvaluationAcceptanceStatusId",
                table: "Evaluation");

            migrationBuilder.DropTable(
                name: "EvaluationAcceptanceStatus");

            migrationBuilder.DropIndex(
                name: "IX_Evaluation_EvaluationAcceptanceStatusId",
                table: "Evaluation");

            migrationBuilder.DropColumn(
                name: "EvaluationAcceptanceStatusId",
                table: "Evaluation");
        }
    }
}
