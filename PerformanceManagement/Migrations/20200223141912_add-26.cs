using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BehaviouralCompetencie",
                columns: table => new
                {
                    BehaviouralCompetencyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    PeriodDefinitoionId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BehaviouralCompetencie", x => x.BehaviouralCompetencyId);
                    table.ForeignKey(
                        name: "FK_BehaviouralCompetencie_PeriodDefinitoion_PeriodDefinitoionId",
                        column: x => x.PeriodDefinitoionId,
                        principalTable: "PeriodDefinitoion",
                        principalColumn: "PeriodDefinitoionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    JobId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.JobId);
                });

            migrationBuilder.CreateTable(
                name: "Truth",
                columns: table => new
                {
                    TruthId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    BehaviouralCompetencyId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Truth", x => x.TruthId);
                    table.ForeignKey(
                        name: "FK_Truth_BehaviouralCompetencie_BehaviouralCompetencyId",
                        column: x => x.BehaviouralCompetencyId,
                        principalTable: "BehaviouralCompetencie",
                        principalColumn: "BehaviouralCompetencyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CorrespondentJob",
                columns: table => new
                {
                    CorrespondentJobId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JobId = table.Column<int>(nullable: false),
                    BehaviouralCompetencyId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrespondentJob", x => x.CorrespondentJobId);
                    table.ForeignKey(
                        name: "FK_CorrespondentJob_BehaviouralCompetencie_BehaviouralCompetencyId",
                        column: x => x.BehaviouralCompetencyId,
                        principalTable: "BehaviouralCompetencie",
                        principalColumn: "BehaviouralCompetencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CorrespondentJob_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BehaviouralCompetencie_PeriodDefinitoionId",
                table: "BehaviouralCompetencie",
                column: "PeriodDefinitoionId");

            migrationBuilder.CreateIndex(
                name: "IX_CorrespondentJob_BehaviouralCompetencyId",
                table: "CorrespondentJob",
                column: "BehaviouralCompetencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CorrespondentJob_JobId",
                table: "CorrespondentJob",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Truth_BehaviouralCompetencyId",
                table: "Truth",
                column: "BehaviouralCompetencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorrespondentJob");

            migrationBuilder.DropTable(
                name: "Truth");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "BehaviouralCompetencie");
        }
    }
}
