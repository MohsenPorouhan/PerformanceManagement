using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add27 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BehaviouralCompetencie_PeriodDefinitoion_PeriodDefinitoionId",
                table: "BehaviouralCompetencie");

            migrationBuilder.DropForeignKey(
                name: "FK_CorrespondentJob_BehaviouralCompetencie_BehaviouralCompetencyId",
                table: "CorrespondentJob");

            migrationBuilder.DropForeignKey(
                name: "FK_Truth_BehaviouralCompetencie_BehaviouralCompetencyId",
                table: "Truth");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BehaviouralCompetencie",
                table: "BehaviouralCompetencie");

            migrationBuilder.RenameTable(
                name: "BehaviouralCompetencie",
                newName: "BehaviouralCompetency");

            migrationBuilder.RenameIndex(
                name: "IX_BehaviouralCompetencie_PeriodDefinitoionId",
                table: "BehaviouralCompetency",
                newName: "IX_BehaviouralCompetency_PeriodDefinitoionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BehaviouralCompetency",
                table: "BehaviouralCompetency",
                column: "BehaviouralCompetencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_BehaviouralCompetency_PeriodDefinitoion_PeriodDefinitoionId",
                table: "BehaviouralCompetency",
                column: "PeriodDefinitoionId",
                principalTable: "PeriodDefinitoion",
                principalColumn: "PeriodDefinitoionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CorrespondentJob_BehaviouralCompetency_BehaviouralCompetencyId",
                table: "CorrespondentJob",
                column: "BehaviouralCompetencyId",
                principalTable: "BehaviouralCompetency",
                principalColumn: "BehaviouralCompetencyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Truth_BehaviouralCompetency_BehaviouralCompetencyId",
                table: "Truth",
                column: "BehaviouralCompetencyId",
                principalTable: "BehaviouralCompetency",
                principalColumn: "BehaviouralCompetencyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BehaviouralCompetency_PeriodDefinitoion_PeriodDefinitoionId",
                table: "BehaviouralCompetency");

            migrationBuilder.DropForeignKey(
                name: "FK_CorrespondentJob_BehaviouralCompetency_BehaviouralCompetencyId",
                table: "CorrespondentJob");

            migrationBuilder.DropForeignKey(
                name: "FK_Truth_BehaviouralCompetency_BehaviouralCompetencyId",
                table: "Truth");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BehaviouralCompetency",
                table: "BehaviouralCompetency");

            migrationBuilder.RenameTable(
                name: "BehaviouralCompetency",
                newName: "BehaviouralCompetencie");

            migrationBuilder.RenameIndex(
                name: "IX_BehaviouralCompetency_PeriodDefinitoionId",
                table: "BehaviouralCompetencie",
                newName: "IX_BehaviouralCompetencie_PeriodDefinitoionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BehaviouralCompetencie",
                table: "BehaviouralCompetencie",
                column: "BehaviouralCompetencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_BehaviouralCompetencie_PeriodDefinitoion_PeriodDefinitoionId",
                table: "BehaviouralCompetencie",
                column: "PeriodDefinitoionId",
                principalTable: "PeriodDefinitoion",
                principalColumn: "PeriodDefinitoionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CorrespondentJob_BehaviouralCompetencie_BehaviouralCompetencyId",
                table: "CorrespondentJob",
                column: "BehaviouralCompetencyId",
                principalTable: "BehaviouralCompetencie",
                principalColumn: "BehaviouralCompetencyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Truth_BehaviouralCompetencie_BehaviouralCompetencyId",
                table: "Truth",
                column: "BehaviouralCompetencyId",
                principalTable: "BehaviouralCompetencie",
                principalColumn: "BehaviouralCompetencyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
