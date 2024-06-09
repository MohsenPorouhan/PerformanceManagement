using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskWeight",
                table: "Evaluation",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CriteriaWeight",
                columns: table => new
                {
                    CriteriaWeightId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Weight = table.Column<int>(nullable: false),
                    EvaluationId = table.Column<int>(nullable: false),
                    CriteriaId = table.Column<int>(nullable: false),
                    RoleId = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriteriaWeight", x => x.CriteriaWeightId);
                    table.ForeignKey(
                        name: "FK_CriteriaWeight_Criteria_CriteriaId",
                        column: x => x.CriteriaId,
                        principalTable: "Criteria",
                        principalColumn: "CriteriaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CriteriaWeight_Evaluation_EvaluationId",
                        column: x => x.EvaluationId,
                        principalTable: "Evaluation",
                        principalColumn: "EvaluationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CriteriaWeight_CriteriaId",
                table: "CriteriaWeight",
                column: "CriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_CriteriaWeight_EvaluationId",
                table: "CriteriaWeight",
                column: "EvaluationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CriteriaWeight");

            migrationBuilder.DropColumn(
                name: "TaskWeight",
                table: "Evaluation");
        }
    }
}
