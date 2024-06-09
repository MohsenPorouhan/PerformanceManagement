using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add29 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PeriodDefinitoionId",
                table: "Task",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Task",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PublicTask",
                columns: table => new
                {
                    PublicTaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TaskId = table.Column<int>(nullable: false),
                    CorrespondentTaskId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicTask", x => x.PublicTaskId);
                    table.ForeignKey(
                        name: "FK_PublicTask_Task_CorrespondentTaskId",
                        column: x => x.CorrespondentTaskId,
                        principalTable: "Task",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PublicTask_Task_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PublicTask_CorrespondentTaskId",
                table: "PublicTask",
                column: "CorrespondentTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicTask_TaskId",
                table: "PublicTask",
                column: "TaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicTask");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Task");

            migrationBuilder.AlterColumn<int>(
                name: "PeriodDefinitoionId",
                table: "Task",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
