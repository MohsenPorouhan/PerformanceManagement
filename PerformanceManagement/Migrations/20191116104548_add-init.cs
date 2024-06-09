using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.Migrations
{
    public partial class addinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(nullable: false),
                    EffectiveStartDate = table.Column<DateTime>(nullable: false),
                    LongName = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    EffectiveEndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => new { x.DepartmentId, x.EffectiveStartDate });
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "evaluationHierarchies",
                columns: table => new
                {
                    EvaluationHierarchyId = table.Column<int>(nullable: false),
                    EffectiveStartDate = table.Column<DateTime>(nullable: false),
                    ParentEvaluationHierarchyId = table.Column<int>(nullable: true),
                    ParentEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    SupervisorId = table.Column<int>(nullable: false),
                    SupervisorEffectiveStartDate = table.Column<DateTime>(nullable: false),
                    EffectiveEndDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<int>(nullable: false),
                    CreatedByEffectiveStartDate = table.Column<DateTime>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedById = table.Column<int>(nullable: true),
                    LastUpdatedByEffectiveStartDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true),
                    DepartmentType = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: false),
                    DepartmentEffectiveStartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evaluationHierarchies", x => new { x.EvaluationHierarchyId, x.EffectiveStartDate });
                    table.ForeignKey(
                        name: "FK_evaluationHierarchies_Departments_DepartmentId_DepartmentEffectiveStartDate",
                        columns: x => new { x.DepartmentId, x.DepartmentEffectiveStartDate },
                        principalTable: "Departments",
                        principalColumns: new[] { "DepartmentId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_evaluationHierarchies_evaluationHierarchies_ParentEvaluationHierarchyId_ParentEffectiveStartDate",
                        columns: x => new { x.ParentEvaluationHierarchyId, x.ParentEffectiveStartDate },
                        principalTable: "evaluationHierarchies",
                        principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PeopleId = table.Column<int>(nullable: false),
                    EffectiveStartDate = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    EffectiveEndDate = table.Column<DateTime>(nullable: true),
                    PositionType = table.Column<int>(nullable: true),
                    EvaluationHierarchyID = table.Column<int>(nullable: true),
                    EvaluationHierarchyDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => new { x.PeopleId, x.EffectiveStartDate });
                    table.ForeignKey(
                        name: "FK_People_evaluationHierarchies_EvaluationHierarchyID_EvaluationHierarchyDate",
                        columns: x => new { x.EvaluationHierarchyID, x.EvaluationHierarchyDate },
                        principalTable: "evaluationHierarchies",
                        principalColumns: new[] { "EvaluationHierarchyId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    PeopleId = table.Column<int>(nullable: true),
                    PeopleEffectiveStartDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    EffectiveStartDate = table.Column<DateTime>(nullable: true),
                    EffectiveEndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_People_PeopleId_PeopleEffectiveStartDate",
                        columns: x => new { x.PeopleId, x.PeopleEffectiveStartDate },
                        principalTable: "People",
                        principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PeopleId_PeopleEffectiveStartDate",
                table: "AspNetUsers",
                columns: new[] { "PeopleId", "PeopleEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_evaluationHierarchies_CreatedById_CreatedByEffectiveStartDate",
                table: "evaluationHierarchies",
                columns: new[] { "CreatedById", "CreatedByEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_evaluationHierarchies_DepartmentId_DepartmentEffectiveStartDate",
                table: "evaluationHierarchies",
                columns: new[] { "DepartmentId", "DepartmentEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_evaluationHierarchies_LastUpdatedById_LastUpdatedByEffectiveStartDate",
                table: "evaluationHierarchies",
                columns: new[] { "LastUpdatedById", "LastUpdatedByEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_evaluationHierarchies_ParentEvaluationHierarchyId_ParentEffectiveStartDate",
                table: "evaluationHierarchies",
                columns: new[] { "ParentEvaluationHierarchyId", "ParentEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_evaluationHierarchies_SupervisorId_SupervisorEffectiveStartDate",
                table: "evaluationHierarchies",
                columns: new[] { "SupervisorId", "SupervisorEffectiveStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_People_EvaluationHierarchyID_EvaluationHierarchyDate",
                table: "People",
                columns: new[] { "EvaluationHierarchyID", "EvaluationHierarchyDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_evaluationHierarchies_People_CreatedById_CreatedByEffectiveStartDate",
                table: "evaluationHierarchies",
                columns: new[] { "CreatedById", "CreatedByEffectiveStartDate" },
                principalTable: "People",
                principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_evaluationHierarchies_People_LastUpdatedById_LastUpdatedByEffectiveStartDate",
                table: "evaluationHierarchies",
                columns: new[] { "LastUpdatedById", "LastUpdatedByEffectiveStartDate" },
                principalTable: "People",
                principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_evaluationHierarchies_People_SupervisorId_SupervisorEffectiveStartDate",
                table: "evaluationHierarchies",
                columns: new[] { "SupervisorId", "SupervisorEffectiveStartDate" },
                principalTable: "People",
                principalColumns: new[] { "PeopleId", "EffectiveStartDate" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_evaluationHierarchies_People_CreatedById_CreatedByEffectiveStartDate",
                table: "evaluationHierarchies");

            migrationBuilder.DropForeignKey(
                name: "FK_evaluationHierarchies_People_LastUpdatedById_LastUpdatedByEffectiveStartDate",
                table: "evaluationHierarchies");

            migrationBuilder.DropForeignKey(
                name: "FK_evaluationHierarchies_People_SupervisorId_SupervisorEffectiveStartDate",
                table: "evaluationHierarchies");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "evaluationHierarchies");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
