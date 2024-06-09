using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LikertScoreWayEffectiveStartDate",
                table: "PeriodDefinitoion",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LikertScoreWayId",
                table: "PeriodDefinitoion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LikertWeightWayBehaiviourEffectiveStartDate",
                table: "PeriodDefinitoion",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LikertWeightWayBehaiviourId",
                table: "PeriodDefinitoion",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LikertWeightWayTaskEffectiveStartDate",
                table: "PeriodDefinitoion",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LikertWeightWayTaskId",
                table: "PeriodDefinitoion",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WeightWayBehaviour",
                table: "PeriodDefinitoion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeightWayScore",
                table: "PeriodDefinitoion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeightWayTask",
                table: "PeriodDefinitoion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LikertScale",
                columns: table => new
                {
                    LikertScaleId = table.Column<int>(nullable: false),
                    EffectiveStartDate = table.Column<DateTime>(nullable: false),
                    EffectiveEndDate = table.Column<DateTime>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikertScale", x => new { x.LikertScaleId, x.EffectiveStartDate });
                });

            migrationBuilder.CreateTable(
                name: "LikertDescribor",
                columns: table => new
                {
                    LikertDescriborId = table.Column<int>(nullable: false),
                    EffectiveStartDate = table.Column<DateTime>(nullable: false),
                    EffectiveEndDate = table.Column<DateTime>(nullable: true),
                    NumberScale = table.Column<int>(nullable: false),
                    TitleScale = table.Column<string>(nullable: true),
                    LikertScaleId = table.Column<int>(nullable: false),
                    LikertScaleEffectiveStartDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikertDescribor", x => new { x.LikertDescriborId, x.EffectiveStartDate });
                    table.ForeignKey(
                        name: "FK_LikertDescribor_LikertScale_LikertScaleId_LikertScaleEffectiveStartDate",
                        columns: x => new { x.LikertScaleId, x.LikertScaleEffectiveStartDate },
                        principalTable: "LikertScale",
                        principalColumns: new[] { "LikertScaleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeriodDefinitoion_LikertScoreWayId_LikertScoreWayEffectiveStartDate",
                table: "PeriodDefinitoion",
                columns: new[] { "LikertScoreWayId", "LikertScoreWayEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_PeriodDefinitoion_LikertWeightWayBehaiviourId_LikertWeightWayBehaiviourEffectiveStartDate",
                table: "PeriodDefinitoion",
                columns: new[] { "LikertWeightWayBehaiviourId", "LikertWeightWayBehaiviourEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_PeriodDefinitoion_LikertWeightWayTaskId_LikertWeightWayTaskEffectiveStartDate",
                table: "PeriodDefinitoion",
                columns: new[] { "LikertWeightWayTaskId", "LikertWeightWayTaskEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_LikertDescribor_LikertScaleId_LikertScaleEffectiveStartDate",
                table: "LikertDescribor",
                columns: new[] { "LikertScaleId", "LikertScaleEffectiveStartDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_PeriodDefinitoion_LikertScale_LikertScoreWayId_LikertScoreWayEffectiveStartDate",
                table: "PeriodDefinitoion",
                columns: new[] { "LikertScoreWayId", "LikertScoreWayEffectiveStartDate" },
                principalTable: "LikertScale",
                principalColumns: new[] { "LikertScaleId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PeriodDefinitoion_LikertScale_LikertWeightWayBehaiviourId_LikertWeightWayBehaiviourEffectiveStartDate",
                table: "PeriodDefinitoion",
                columns: new[] { "LikertWeightWayBehaiviourId", "LikertWeightWayBehaiviourEffectiveStartDate" },
                principalTable: "LikertScale",
                principalColumns: new[] { "LikertScaleId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PeriodDefinitoion_LikertScale_LikertWeightWayTaskId_LikertWeightWayTaskEffectiveStartDate",
                table: "PeriodDefinitoion",
                columns: new[] { "LikertWeightWayTaskId", "LikertWeightWayTaskEffectiveStartDate" },
                principalTable: "LikertScale",
                principalColumns: new[] { "LikertScaleId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeriodDefinitoion_LikertScale_LikertScoreWayId_LikertScoreWayEffectiveStartDate",
                table: "PeriodDefinitoion");

            migrationBuilder.DropForeignKey(
                name: "FK_PeriodDefinitoion_LikertScale_LikertWeightWayBehaiviourId_LikertWeightWayBehaiviourEffectiveStartDate",
                table: "PeriodDefinitoion");

            migrationBuilder.DropForeignKey(
                name: "FK_PeriodDefinitoion_LikertScale_LikertWeightWayTaskId_LikertWeightWayTaskEffectiveStartDate",
                table: "PeriodDefinitoion");

            migrationBuilder.DropTable(
                name: "LikertDescribor");

            migrationBuilder.DropTable(
                name: "LikertScale");

            migrationBuilder.DropIndex(
                name: "IX_PeriodDefinitoion_LikertScoreWayId_LikertScoreWayEffectiveStartDate",
                table: "PeriodDefinitoion");

            migrationBuilder.DropIndex(
                name: "IX_PeriodDefinitoion_LikertWeightWayBehaiviourId_LikertWeightWayBehaiviourEffectiveStartDate",
                table: "PeriodDefinitoion");

            migrationBuilder.DropIndex(
                name: "IX_PeriodDefinitoion_LikertWeightWayTaskId_LikertWeightWayTaskEffectiveStartDate",
                table: "PeriodDefinitoion");

            migrationBuilder.DropColumn(
                name: "LikertScoreWayEffectiveStartDate",
                table: "PeriodDefinitoion");

            migrationBuilder.DropColumn(
                name: "LikertScoreWayId",
                table: "PeriodDefinitoion");

            migrationBuilder.DropColumn(
                name: "LikertWeightWayBehaiviourEffectiveStartDate",
                table: "PeriodDefinitoion");

            migrationBuilder.DropColumn(
                name: "LikertWeightWayBehaiviourId",
                table: "PeriodDefinitoion");

            migrationBuilder.DropColumn(
                name: "LikertWeightWayTaskEffectiveStartDate",
                table: "PeriodDefinitoion");

            migrationBuilder.DropColumn(
                name: "LikertWeightWayTaskId",
                table: "PeriodDefinitoion");

            migrationBuilder.DropColumn(
                name: "WeightWayBehaviour",
                table: "PeriodDefinitoion");

            migrationBuilder.DropColumn(
                name: "WeightWayScore",
                table: "PeriodDefinitoion");

            migrationBuilder.DropColumn(
                name: "WeightWayTask",
                table: "PeriodDefinitoion");
        }
    }
}
