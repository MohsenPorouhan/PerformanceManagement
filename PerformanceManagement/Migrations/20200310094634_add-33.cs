using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvaluationParticipant",
                columns: table => new
                {
                    EvaluationParticipantId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Score = table.Column<int>(nullable: true),
                    Confirmation = table.Column<bool>(nullable: true),
                    RequestDate = table.Column<DateTime>(nullable: false),
                    ResponseDate = table.Column<DateTime>(nullable: true),
                    EvaluationId = table.Column<int>(nullable: false),
                    ParticipantId = table.Column<int>(nullable: false),
                    PeopleEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    ParticipantEvalHieEffcStartDate = table.Column<DateTime>(nullable: true),
                    ParticipantEvaluationHierarchyId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationParticipant", x => x.EvaluationParticipantId);
                    table.ForeignKey(
                        name: "FK_EvaluationParticipant_Evaluation_EvaluationId",
                        column: x => x.EvaluationId,
                        principalTable: "Evaluation",
                        principalColumn: "EvaluationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationParticipant_evaluationHierarchies_ParticipantEvaluationHierarchyId_ParticipantEvalHieEffcStartDate",
                        columns: x => new { x.ParticipantEvaluationHierarchyId, x.ParticipantEvalHieEffcStartDate },
                        principalTable: "evaluationHierarchies",
                        principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationParticipant_People_ParticipantId_PeopleEffectiveStartDate",
                        columns: x => new { x.ParticipantId, x.PeopleEffectiveStartDate },
                        principalTable: "People",
                        principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationParticipant_EvaluationId",
                table: "EvaluationParticipant",
                column: "EvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationParticipant_ParticipantEvaluationHierarchyId_ParticipantEvalHieEffcStartDate",
                table: "EvaluationParticipant",
                columns: new[] { "ParticipantEvaluationHierarchyId", "ParticipantEvalHieEffcStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationParticipant_ParticipantId_PeopleEffectiveStartDate",
                table: "EvaluationParticipant",
                columns: new[] { "ParticipantId", "PeopleEffectiveStartDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationParticipant");
        }
    }
}
