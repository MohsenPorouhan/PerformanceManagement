using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class add70 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Protest",
                columns: table => new
                {
                    ProtestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Confirmation = table.Column<bool>(nullable: true),
                    VisibilityToHierarchy = table.Column<bool>(nullable: false),
                    ProtesterId = table.Column<int>(nullable: false),
                    ProtesterEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    ProtesterDepartmentId = table.Column<int>(nullable: false),
                    ProtesterDepartmnetEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    PeriodDefinitionId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Protest", x => x.ProtestId);
                    table.ForeignKey(
                        name: "FK_Protest_PeriodDefinitoion_PeriodDefinitionId",
                        column: x => x.PeriodDefinitionId,
                        principalTable: "PeriodDefinitoion",
                        principalColumn: "PeriodDefinitoionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Protest_evaluationHierarchies_ProtesterDepartmentId_ProtesterDepartmnetEffectiveStartDate",
                        columns: x => new { x.ProtesterDepartmentId, x.ProtesterDepartmnetEffectiveStartDate },
                        principalTable: "evaluationHierarchies",
                        principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Protest_People_ProtesterId_ProtesterEffectiveStartDate",
                        columns: x => new { x.ProtesterId, x.ProtesterEffectiveStartDate },
                        principalTable: "People",
                        principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Addressee",
                columns: table => new
                {
                    AddresseeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CoacherLevel = table.Column<int>(nullable: false),
                    ProtestId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addressee", x => x.AddresseeId);
                    table.ForeignKey(
                        name: "FK_Addressee_Protest_ProtestId",
                        column: x => x.ProtestId,
                        principalTable: "Protest",
                        principalColumn: "ProtestId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProtestResponse",
                columns: table => new
                {
                    ProtestResponseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProtestId = table.Column<int>(nullable: false),
                    Response = table.Column<string>(nullable: true),
                    CoacherLevel = table.Column<int>(nullable: true),
                    PersonId = table.Column<int>(nullable: false),
                    PersonEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    PersonPeopleId = table.Column<int>(nullable: true),
                    PersonEffectiveStartDate1 = table.Column<DateTime>(nullable: true),
                    PersonDepartmentId = table.Column<int>(nullable: true),
                    PersonDepartmnetEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    PersonDepartmentEvaluationHierarchyId = table.Column<int>(nullable: true),
                    PersonDepartmentEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    RoleType = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProtestResponse", x => x.ProtestResponseId);
                    table.ForeignKey(
                        name: "FK_ProtestResponse_Protest_ProtestId",
                        column: x => x.ProtestId,
                        principalTable: "Protest",
                        principalColumn: "ProtestId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProtestResponse_evaluationHierarchies_PersonDepartmentEvaluationHierarchyId_PersonDepartmentEffectiveStartDate",
                        columns: x => new { x.PersonDepartmentEvaluationHierarchyId, x.PersonDepartmentEffectiveStartDate },
                        principalTable: "evaluationHierarchies",
                        principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProtestResponse_People_PersonPeopleId_PersonEffectiveStartDate1",
                        columns: x => new { x.PersonPeopleId, x.PersonEffectiveStartDate1 },
                        principalTable: "People",
                        principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 18, 13, 11, 4, 434, DateTimeKind.Local).AddTicks(8140));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 18, 13, 11, 4, 437, DateTimeKind.Local).AddTicks(3569));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 18, 13, 11, 4, 437, DateTimeKind.Local).AddTicks(3601));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 18, 13, 11, 4, 437, DateTimeKind.Local).AddTicks(3611));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 18, 13, 11, 4, 442, DateTimeKind.Local).AddTicks(9901));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 18, 13, 11, 4, 443, DateTimeKind.Local).AddTicks(636));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 18, 13, 11, 4, 443, DateTimeKind.Local).AddTicks(651));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 18, 13, 11, 4, 443, DateTimeKind.Local).AddTicks(655));

            migrationBuilder.CreateIndex(
                name: "IX_Addressee_ProtestId",
                table: "Addressee",
                column: "ProtestId");

            migrationBuilder.CreateIndex(
                name: "IX_Protest_PeriodDefinitionId",
                table: "Protest",
                column: "PeriodDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Protest_ProtesterDepartmentId_ProtesterDepartmnetEffectiveStartDate",
                table: "Protest",
                columns: new[] { "ProtesterDepartmentId", "ProtesterDepartmnetEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Protest_ProtesterId_ProtesterEffectiveStartDate",
                table: "Protest",
                columns: new[] { "ProtesterId", "ProtesterEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_ProtestResponse_ProtestId",
                table: "ProtestResponse",
                column: "ProtestId");

            migrationBuilder.CreateIndex(
                name: "IX_ProtestResponse_PersonDepartmentEvaluationHierarchyId_PersonDepartmentEffectiveStartDate",
                table: "ProtestResponse",
                columns: new[] { "PersonDepartmentEvaluationHierarchyId", "PersonDepartmentEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_ProtestResponse_PersonPeopleId_PersonEffectiveStartDate1",
                table: "ProtestResponse",
                columns: new[] { "PersonPeopleId", "PersonEffectiveStartDate1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addressee");

            migrationBuilder.DropTable(
                name: "ProtestResponse");

            migrationBuilder.DropTable(
                name: "Protest");

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 13, 26, 32, 523, DateTimeKind.Local).AddTicks(9871));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 13, 26, 32, 531, DateTimeKind.Local).AddTicks(2201));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 13, 26, 32, 531, DateTimeKind.Local).AddTicks(2244));

            migrationBuilder.UpdateData(
                table: "EvaluationAcceptanceStatus",
                keyColumn: "EvaluationAcceptanceStatusId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 13, 26, 32, 531, DateTimeKind.Local).AddTicks(2256));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 13, 26, 32, 542, DateTimeKind.Local).AddTicks(1845));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 13, 26, 32, 542, DateTimeKind.Local).AddTicks(3717));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 13, 26, 32, 542, DateTimeKind.Local).AddTicks(3756));

            migrationBuilder.UpdateData(
                table: "NotificationTitle",
                keyColumn: "NotificationTitleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2020, 8, 15, 13, 26, 32, 542, DateTimeKind.Local).AddTicks(3766));
        }
    }
}
