using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CoacherLevel",
                table: "EvaluationScore",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CoacherId",
                table: "EvaluationScore",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "EvaluationScore",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CoacherLevel",
                table: "CriteriaWeightScore",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CoacherId",
                table: "CriteriaWeightScore",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "CriteriaWeightScore",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "EvaluationScore");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "CriteriaWeightScore");

            migrationBuilder.AlterColumn<int>(
                name: "CoacherLevel",
                table: "EvaluationScore",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CoacherId",
                table: "EvaluationScore",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CoacherLevel",
                table: "CriteriaWeightScore",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CoacherId",
                table: "CriteriaWeightScore",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
