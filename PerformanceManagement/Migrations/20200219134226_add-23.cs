using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CriteriaWeightScore",
                columns: table => new
                {
                    CriteriaWeightScoreId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Score = table.Column<int>(nullable: false),
                    CriteriaWeightId = table.Column<int>(nullable: false),
                    CoacherId = table.Column<int>(nullable: false),
                    CoacherEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    CoacherLevel = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriteriaWeightScore", x => x.CriteriaWeightScoreId);
                    table.ForeignKey(
                        name: "FK_CriteriaWeightScore_CriteriaWeight_CriteriaWeightId",
                        column: x => x.CriteriaWeightId,
                        principalTable: "CriteriaWeight",
                        principalColumn: "CriteriaWeightId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CriteriaWeightScore_People_CoacherId_CoacherEffectiveStartDate",
                        columns: x => new { x.CoacherId, x.CoacherEffectiveStartDate },
                        principalTable: "People",
                        principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationScore",
                columns: table => new
                {
                    EvaluationScoreId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Score = table.Column<int>(nullable: false),
                    EvaluationId = table.Column<int>(nullable: false),
                    CoacherId = table.Column<int>(nullable: false),
                    CoacherEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    CoacherLevel = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationScore", x => x.EvaluationScoreId);
                    table.ForeignKey(
                        name: "FK_EvaluationScore_Evaluation_EvaluationId",
                        column: x => x.EvaluationId,
                        principalTable: "Evaluation",
                        principalColumn: "EvaluationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationScore_People_CoacherId_CoacherEffectiveStartDate",
                        columns: x => new { x.CoacherId, x.CoacherEffectiveStartDate },
                        principalTable: "People",
                        principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CriteriaWeightScore_CriteriaWeightId",
                table: "CriteriaWeightScore",
                column: "CriteriaWeightId");

            migrationBuilder.CreateIndex(
                name: "IX_CriteriaWeightScore_CoacherId_CoacherEffectiveStartDate",
                table: "CriteriaWeightScore",
                columns: new[] { "CoacherId", "CoacherEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationScore_EvaluationId",
                table: "EvaluationScore",
                column: "EvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationScore_CoacherId_CoacherEffectiveStartDate",
                table: "EvaluationScore",
                columns: new[] { "CoacherId", "CoacherEffectiveStartDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CriteriaWeightScore");

            migrationBuilder.DropTable(
                name: "EvaluationScore");
        }
    }
}
