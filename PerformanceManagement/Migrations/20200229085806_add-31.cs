using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add31 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvaluationBehaviouralCompetency",
                columns: table => new
                {
                    EvaluationBehaviouralCompetencyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BehaviouralCompetencyId = table.Column<int>(nullable: false),
                    BehaviouralCompetencyWeight = table.Column<int>(nullable: true),
                    AllocatorEvalBehavHieEffcStartDate = table.Column<DateTime>(nullable: true),
                    AllocatorEvaluationBehaviouralHierarchyId = table.Column<int>(nullable: true),
                    RecieverAllocEvalBehavHieEffcStartDate = table.Column<DateTime>(nullable: true),
                    RecieverAllocationEvaluationBehaviouralHierarchyId = table.Column<int>(nullable: false),
                    AllocatorPersonEffecStartDate = table.Column<DateTime>(nullable: true),
                    AllocatorPersonId = table.Column<int>(nullable: false),
                    AllocatorRoleId = table.Column<string>(nullable: true),
                    RecieverAllocPersonEffecStartDate = table.Column<DateTime>(nullable: true),
                    RecieverAllocationPersonId = table.Column<int>(nullable: false),
                    PeriodDefinitoionId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationBehaviouralCompetency", x => x.EvaluationBehaviouralCompetencyId);
                    table.ForeignKey(
                        name: "FK_EvaluationBehaviouralCompetency_BehaviouralCompetency_BehaviouralCompetencyId",
                        column: x => x.BehaviouralCompetencyId,
                        principalTable: "BehaviouralCompetency",
                        principalColumn: "BehaviouralCompetencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationBehaviouralCompetency_PeriodDefinitoion_PeriodDefinitoionId",
                        column: x => x.PeriodDefinitoionId,
                        principalTable: "PeriodDefinitoion",
                        principalColumn: "PeriodDefinitoionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationBehaviouralCompetency_evaluationHierarchies_AllocatorEvaluationBehaviouralHierarchyId_AllocatorEvalBehavHieEffcSta~",
                        columns: x => new { x.AllocatorEvaluationBehaviouralHierarchyId, x.AllocatorEvalBehavHieEffcStartDate },
                        principalTable: "evaluationHierarchies",
                        principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationBehaviouralCompetency_People_AllocatorPersonId_AllocatorPersonEffecStartDate",
                        columns: x => new { x.AllocatorPersonId, x.AllocatorPersonEffecStartDate },
                        principalTable: "People",
                        principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationBehaviouralCompetency_evaluationHierarchies_RecieverAllocationEvaluationBehaviouralHierarchyId_RecieverAllocEvalBe~",
                        columns: x => new { x.RecieverAllocationEvaluationBehaviouralHierarchyId, x.RecieverAllocEvalBehavHieEffcStartDate },
                        principalTable: "evaluationHierarchies",
                        principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationBehaviouralCompetency_People_RecieverAllocationPersonId_RecieverAllocPersonEffecStartDate",
                        columns: x => new { x.RecieverAllocationPersonId, x.RecieverAllocPersonEffecStartDate },
                        principalTable: "People",
                        principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationBehaviouralParticipant",
                columns: table => new
                {
                    EvaluationBehaviouralParticipantId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Score = table.Column<int>(nullable: false),
                    Confirmation = table.Column<bool>(nullable: false),
                    RequestDate = table.Column<DateTime>(nullable: false),
                    ResponseDate = table.Column<DateTime>(nullable: true),
                    EvaluationBehaviouralCompetencyId = table.Column<int>(nullable: false),
                    ParticipantId = table.Column<int>(nullable: false),
                    PeopleEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    ParticipantEvalBehavHieEffcStartDate = table.Column<DateTime>(nullable: true),
                    ParticipantEvaluationBehaviouralHierarchyId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationBehaviouralParticipant", x => x.EvaluationBehaviouralParticipantId);
                    table.ForeignKey(
                        name: "FK_EvaluationBehaviouralParticipant_EvaluationBehaviouralCompetency_EvaluationBehaviouralCompetencyId",
                        column: x => x.EvaluationBehaviouralCompetencyId,
                        principalTable: "EvaluationBehaviouralCompetency",
                        principalColumn: "EvaluationBehaviouralCompetencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationBehaviouralParticipant_evaluationHierarchies_ParticipantEvaluationBehaviouralHierarchyId_ParticipantEvalBehavHieEf~",
                        columns: x => new { x.ParticipantEvaluationBehaviouralHierarchyId, x.ParticipantEvalBehavHieEffcStartDate },
                        principalTable: "evaluationHierarchies",
                        principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationBehaviouralParticipant_People_ParticipantId_PeopleEffectiveStartDate",
                        columns: x => new { x.ParticipantId, x.PeopleEffectiveStartDate },
                        principalTable: "People",
                        principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationBehaviourCompetencyScore",
                columns: table => new
                {
                    EvaluationBehaviourCompetencyScoreId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Score = table.Column<int>(nullable: false),
                    EvaluationBehaviouralCompetencyId = table.Column<int>(nullable: false),
                    CoacherId = table.Column<int>(nullable: true),
                    CoacherEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    CoacherLevel = table.Column<int>(nullable: true),
                    RoleId = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationBehaviourCompetencyScore", x => x.EvaluationBehaviourCompetencyScoreId);
                    table.ForeignKey(
                        name: "FK_EvaluationBehaviourCompetencyScore_EvaluationBehaviouralCompetency_EvaluationBehaviouralCompetencyId",
                        column: x => x.EvaluationBehaviouralCompetencyId,
                        principalTable: "EvaluationBehaviouralCompetency",
                        principalColumn: "EvaluationBehaviouralCompetencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationBehaviourCompetencyScore_People_CoacherId_CoacherEffectiveStartDate",
                        columns: x => new { x.CoacherId, x.CoacherEffectiveStartDate },
                        principalTable: "People",
                        principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationBehaviouralCompetency_BehaviouralCompetencyId",
                table: "EvaluationBehaviouralCompetency",
                column: "BehaviouralCompetencyId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationBehaviouralCompetency_PeriodDefinitoionId",
                table: "EvaluationBehaviouralCompetency",
                column: "PeriodDefinitoionId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationBehaviouralCompetency_AllocatorEvaluationBehaviouralHierarchyId_AllocatorEvalBehavHieEffcStartDate",
                table: "EvaluationBehaviouralCompetency",
                columns: new[] { "AllocatorEvaluationBehaviouralHierarchyId", "AllocatorEvalBehavHieEffcStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationBehaviouralCompetency_AllocatorPersonId_AllocatorPersonEffecStartDate",
                table: "EvaluationBehaviouralCompetency",
                columns: new[] { "AllocatorPersonId", "AllocatorPersonEffecStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationBehaviouralCompetency_RecieverAllocationEvaluationBehaviouralHierarchyId_RecieverAllocEvalBehavHieEffcStartDate",
                table: "EvaluationBehaviouralCompetency",
                columns: new[] { "RecieverAllocationEvaluationBehaviouralHierarchyId", "RecieverAllocEvalBehavHieEffcStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationBehaviouralCompetency_RecieverAllocationPersonId_RecieverAllocPersonEffecStartDate",
                table: "EvaluationBehaviouralCompetency",
                columns: new[] { "RecieverAllocationPersonId", "RecieverAllocPersonEffecStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationBehaviouralParticipant_EvaluationBehaviouralCompetencyId",
                table: "EvaluationBehaviouralParticipant",
                column: "EvaluationBehaviouralCompetencyId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationBehaviouralParticipant_ParticipantEvaluationBehaviouralHierarchyId_ParticipantEvalBehavHieEffcStartDate",
                table: "EvaluationBehaviouralParticipant",
                columns: new[] { "ParticipantEvaluationBehaviouralHierarchyId", "ParticipantEvalBehavHieEffcStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationBehaviouralParticipant_ParticipantId_PeopleEffectiveStartDate",
                table: "EvaluationBehaviouralParticipant",
                columns: new[] { "ParticipantId", "PeopleEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationBehaviourCompetencyScore_EvaluationBehaviouralCompetencyId",
                table: "EvaluationBehaviourCompetencyScore",
                column: "EvaluationBehaviouralCompetencyId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationBehaviourCompetencyScore_CoacherId_CoacherEffectiveStartDate",
                table: "EvaluationBehaviourCompetencyScore",
                columns: new[] { "CoacherId", "CoacherEffectiveStartDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationBehaviouralParticipant");

            migrationBuilder.DropTable(
                name: "EvaluationBehaviourCompetencyScore");

            migrationBuilder.DropTable(
                name: "EvaluationBehaviouralCompetency");
        }
    }
}
