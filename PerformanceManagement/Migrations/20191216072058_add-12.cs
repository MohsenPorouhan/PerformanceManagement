using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LikertScale",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LikertScale_Name",
                table: "LikertScale",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LikertScale_Name",
                table: "LikertScale");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LikertScale",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
