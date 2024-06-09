﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PerformanceManagement.Models;

namespace PerformanceManagement.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200201131614_add-21")]
    partial class add21
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("PerformanceManagement.Models.Coacher.ChartConfirmation", b =>
                {
                    b.Property<int>("ChartConfirmationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EvaluationHierarchyId");

                    b.Property<int>("PeriodDefinitionId");

                    b.Property<string>("CauseDescription");

                    b.Property<bool>("Confirmation");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime?>("EvalHieEffectiveStartDate");

                    b.Property<int?>("LastUpdatedBy");

                    b.Property<DateTime?>("LastUpdatedDate");

                    b.Property<DateTime?>("SupervisorEffectiveStartDate");

                    b.Property<int>("SupervisorId");

                    b.HasKey("ChartConfirmationId", "EvaluationHierarchyId", "PeriodDefinitionId");

                    b.HasIndex("PeriodDefinitionId");

                    b.HasIndex("EvaluationHierarchyId", "EvalHieEffectiveStartDate");

                    b.HasIndex("SupervisorId", "SupervisorEffectiveStartDate");

                    b.ToTable("ChartConfirmation");
                });

            modelBuilder.Entity("PerformanceManagement.Models.Criteria", b =>
                {
                    b.Property<int>("CriteriaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("LastUpdatedBy");

                    b.Property<DateTime?>("LastUpdatedDate");

                    b.Property<string>("LimitOfAdmission");

                    b.Property<int>("TaskId");

                    b.Property<string>("Title");

                    b.HasKey("CriteriaId");

                    b.HasIndex("TaskId");

                    b.ToTable("Criteria");
                });

            modelBuilder.Entity("PerformanceManagement.Models.Evaluation", b =>
                {
                    b.Property<int>("EvaluationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("AllocatorEvalHieEffcStartDate");

                    b.Property<int?>("AllocatorEvaluationHierarchyId");

                    b.Property<DateTime?>("AllocatorPersonEffecStartDate");

                    b.Property<int>("AllocatorPersonId");

                    b.Property<string>("AllocatorRoleId");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("LastUpdatedBy");

                    b.Property<DateTime?>("LastUpdatedDate");

                    b.Property<int>("PeriodDefinitoionId");

                    b.Property<DateTime?>("RecieverAllocEvalHieEffcStartDate");

                    b.Property<DateTime?>("RecieverAllocPersonEffecStartDate");

                    b.Property<int>("RecieverAllocationEvaluationHierarchyId");

                    b.Property<int>("RecieverAllocationPersonId");

                    b.Property<int>("TaskId");

                    b.HasKey("EvaluationId");

                    b.HasIndex("PeriodDefinitoionId");

                    b.HasIndex("TaskId");

                    b.HasIndex("AllocatorEvaluationHierarchyId", "AllocatorEvalHieEffcStartDate");

                    b.HasIndex("AllocatorPersonId", "AllocatorPersonEffecStartDate");

                    b.HasIndex("RecieverAllocationEvaluationHierarchyId", "RecieverAllocEvalHieEffcStartDate");

                    b.HasIndex("RecieverAllocationPersonId", "RecieverAllocPersonEffecStartDate");

                    b.ToTable("Evaluation");
                });

            modelBuilder.Entity("PerformanceManagement.Models.HRAdmin.Department", b =>
                {
                    b.Property<int>("DepartmentId");

                    b.Property<DateTime>("EffectiveStartDate");

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime?>("EffectiveEndDate");

                    b.Property<string>("LongName");

                    b.Property<string>("ShortName");

                    b.HasKey("DepartmentId", "EffectiveStartDate");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("PerformanceManagement.Models.HRAdmin.EvaluationCoefficient", b =>
                {
                    b.Property<int>("EvaluationCoefficientId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("level1CoacherBWith");

                    b.Property<int>("level1CoacherBWithout");

                    b.Property<int>("level1CoacherTWith");

                    b.Property<int>("level1CoacherTWithout");

                    b.Property<int>("level2CoacherBWith");

                    b.Property<int>("level2CoacherBWithout");

                    b.Property<int>("level2CoacherTWith");

                    b.Property<int>("level2CoacherTWithout");

                    b.Property<int>("participantCoefficientB");

                    b.Property<int>("participantCoefficientT");

                    b.Property<int>("selfEvaluationBWith");

                    b.Property<int>("selfEvaluationBWithout");

                    b.Property<int>("selfEvaluationTWith");

                    b.Property<int>("selfEvaluationTWithout");

                    b.HasKey("EvaluationCoefficientId");

                    b.ToTable("EvaluationCoefficient");
                });

            modelBuilder.Entity("PerformanceManagement.Models.HRAdmin.EvaluationHierarchy", b =>
                {
                    b.Property<int>("EvaluationHierarchyId");

                    b.Property<DateTime>("EffectiveStartDate");

                    b.Property<DateTime?>("CreatedByEffectiveStartDate");

                    b.Property<int>("CreatedById");

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime?>("DepartmentEffectiveStartDate");

                    b.Property<int>("DepartmentId");

                    b.Property<int>("DepartmentType");

                    b.Property<DateTime?>("EffectiveEndDate");

                    b.Property<DateTime?>("LastUpdatedByEffectiveStartDate");

                    b.Property<int?>("LastUpdatedById");

                    b.Property<DateTime?>("LastUpdatedDate");

                    b.Property<DateTime?>("ParentEffectiveStartDate");

                    b.Property<int?>("ParentEvaluationHierarchyId");

                    b.Property<DateTime?>("SupervisorEffectiveStartDate");

                    b.Property<int>("SupervisorId");

                    b.HasKey("EvaluationHierarchyId", "EffectiveStartDate");

                    b.HasIndex("CreatedById", "CreatedByEffectiveStartDate");

                    b.HasIndex("DepartmentId", "DepartmentEffectiveStartDate");

                    b.HasIndex("LastUpdatedById", "LastUpdatedByEffectiveStartDate");

                    b.HasIndex("ParentEvaluationHierarchyId", "ParentEffectiveStartDate");

                    b.HasIndex("SupervisorId", "SupervisorEffectiveStartDate");

                    b.ToTable("evaluationHierarchies");
                });

            modelBuilder.Entity("PerformanceManagement.Models.HRAdmin.LikertDescribor", b =>
                {
                    b.Property<int>("LikertDescriborId");

                    b.Property<DateTime>("EffectiveStartDate");

                    b.Property<DateTime?>("EffectiveEndDate");

                    b.Property<DateTime?>("LikertScaleEffectiveStartDate");

                    b.Property<int>("LikertScaleId");

                    b.Property<int>("NumberScale");

                    b.Property<string>("TitleScale");

                    b.HasKey("LikertDescriborId", "EffectiveStartDate");

                    b.HasIndex("LikertScaleId", "LikertScaleEffectiveStartDate");

                    b.ToTable("LikertDescribor");
                });

            modelBuilder.Entity("PerformanceManagement.Models.HRAdmin.LikertScale", b =>
                {
                    b.Property<int>("LikertScaleId");

                    b.Property<DateTime>("EffectiveStartDate");

                    b.Property<DateTime?>("EffectiveEndDate");

                    b.Property<string>("Name");

                    b.Property<int>("Type");

                    b.HasKey("LikertScaleId", "EffectiveStartDate");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("LikertScale");
                });

            modelBuilder.Entity("PerformanceManagement.Models.HRAdmin.PeriodDefinitoion", b =>
                {
                    b.Property<int>("PeriodDefinitoionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float?>("CompetencyPercent");

                    b.Property<DateTime>("DateFrom");

                    b.Property<DateTime>("DateTo");

                    b.Property<int?>("EvaluationCoefficientId");

                    b.Property<DateTime?>("LikertScoreWayEffectiveStartDate");

                    b.Property<int?>("LikertScoreWayId");

                    b.Property<DateTime?>("LikertWeightWayBehaiviourEffectiveStartDate");

                    b.Property<int?>("LikertWeightWayBehaiviourId");

                    b.Property<DateTime?>("LikertWeightWayTaskEffectiveStartDate");

                    b.Property<int?>("LikertWeightWayTaskId");

                    b.Property<int>("PeriodCode");

                    b.Property<string>("PeriodTitle")
                        .IsRequired();

                    b.Property<float?>("TaskPercent");

                    b.Property<int?>("WeightWayBehaviour");

                    b.Property<int?>("WeightWayScore");

                    b.Property<int?>("WeightWayTask");

                    b.HasKey("PeriodDefinitoionId");

                    b.HasIndex("EvaluationCoefficientId")
                        .IsUnique()
                        .HasFilter("[EvaluationCoefficientId] IS NOT NULL");

                    b.HasIndex("PeriodCode")
                        .IsUnique();

                    b.HasIndex("PeriodTitle")
                        .IsUnique();

                    b.HasIndex("LikertScoreWayId", "LikertScoreWayEffectiveStartDate");

                    b.HasIndex("LikertWeightWayBehaiviourId", "LikertWeightWayBehaiviourEffectiveStartDate");

                    b.HasIndex("LikertWeightWayTaskId", "LikertWeightWayTaskEffectiveStartDate");

                    b.ToTable("PeriodDefinitoion");
                });

            modelBuilder.Entity("PerformanceManagement.Models.HRAdmin.PersonPeriodEvaluationHierarchy", b =>
                {
                    b.Property<int>("PersonPeriodEvaluationHierarchyId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("EvaluationHierarchyEffectiveStartDate");

                    b.Property<int>("EvaluationHierarchyId");

                    b.Property<DateTime?>("PeopleEffectiveStartDate");

                    b.Property<int>("PeopleId");

                    b.Property<int>("PeriodDefinitoionId");

                    b.HasKey("PersonPeriodEvaluationHierarchyId");

                    b.HasIndex("PeriodDefinitoionId");

                    b.HasIndex("EvaluationHierarchyId", "EvaluationHierarchyEffectiveStartDate");

                    b.HasIndex("PeopleId", "PeopleEffectiveStartDate");

                    b.ToTable("PersonPeriodEvaluationHierarchy");
                });

            modelBuilder.Entity("PerformanceManagement.Models.HRAdmin.SectionPeriod", b =>
                {
                    b.Property<int>("SectionPeriodId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateFrom");

                    b.Property<DateTime>("DateTo");

                    b.Property<int>("PeriodDefinitoionId");

                    b.Property<int>("StatusCode");

                    b.HasKey("SectionPeriodId");

                    b.HasIndex("PeriodDefinitoionId");

                    b.ToTable("SectionPeriod");
                });

            modelBuilder.Entity("PerformanceManagement.Models.People", b =>
                {
                    b.Property<int>("PeopleId");

                    b.Property<DateTime>("EffectiveStartDate");

                    b.Property<DateTime?>("EffectiveEndDate");

                    b.Property<DateTime?>("EvaluationHierarchyDate");

                    b.Property<int?>("EvaluationHierarchyID");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int?>("PositionType");

                    b.Property<int?>("changeStatus");

                    b.HasKey("PeopleId", "EffectiveStartDate");

                    b.HasIndex("EvaluationHierarchyID", "EvaluationHierarchyDate");

                    b.ToTable("People");
                });

            modelBuilder.Entity("PerformanceManagement.Models.Task", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsActive");

                    b.Property<int?>("LastUpdatedBy");

                    b.Property<DateTime?>("LastUpdatedDate");

                    b.Property<int?>("ParentTaskId");

                    b.Property<int>("PeriodDefinitoionId");

                    b.Property<string>("RoleId");

                    b.Property<string>("Title");

                    b.Property<int>("Type");

                    b.HasKey("TaskId");

                    b.HasIndex("ParentTaskId");

                    b.HasIndex("PeriodDefinitoionId");

                    b.ToTable("Task");
                });

            modelBuilder.Entity("PerformanceManagement.Models.ICTAdmin.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("EffectiveEndDate");

                    b.Property<DateTime>("EffectiveStartDate");

                    b.Property<DateTime?>("PeopleEffectiveStartDate");

                    b.Property<int?>("PeopleId");

                    b.HasIndex("PeopleId", "PeopleEffectiveStartDate");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PerformanceManagement.Models.Coacher.ChartConfirmation", b =>
                {
                    b.HasOne("PerformanceManagement.Models.HRAdmin.PeriodDefinitoion", "PeriodDefinitoion")
                        .WithMany("ChartConfirmations")
                        .HasForeignKey("PeriodDefinitionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PerformanceManagement.Models.HRAdmin.EvaluationHierarchy", "EvaluationHierarchy")
                        .WithMany("ChartConfirmations")
                        .HasForeignKey("EvaluationHierarchyId", "EvalHieEffectiveStartDate")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PerformanceManagement.Models.People", "Supervisor")
                        .WithMany("ChartConfirmations")
                        .HasForeignKey("SupervisorId", "SupervisorEffectiveStartDate")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("PerformanceManagement.Models.Criteria", b =>
                {
                    b.HasOne("PerformanceManagement.Models.Task", "Task")
                        .WithMany("Criterias")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("PerformanceManagement.Models.Evaluation", b =>
                {
                    b.HasOne("PerformanceManagement.Models.HRAdmin.PeriodDefinitoion", "PeriodDefinitoion")
                        .WithMany("Evaluations")
                        .HasForeignKey("PeriodDefinitoionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PerformanceManagement.Models.Task", "Task")
                        .WithMany("Evaluations")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PerformanceManagement.Models.HRAdmin.EvaluationHierarchy", "AllocatorEvaluationHierarchy")
                        .WithMany("AllocatorEvaluationHierarchies")
                        .HasForeignKey("AllocatorEvaluationHierarchyId", "AllocatorEvalHieEffcStartDate")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PerformanceManagement.Models.People", "Allocator")
                        .WithMany("Allocators")
                        .HasForeignKey("AllocatorPersonId", "AllocatorPersonEffecStartDate")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PerformanceManagement.Models.HRAdmin.EvaluationHierarchy", "RecieverAllocationEvaluationHierarchy")
                        .WithMany("RecieverAllocationEvaluationHierarchies")
                        .HasForeignKey("RecieverAllocationEvaluationHierarchyId", "RecieverAllocEvalHieEffcStartDate")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PerformanceManagement.Models.People", "RecieverAllocation")
                        .WithMany("RecieversAllocation")
                        .HasForeignKey("RecieverAllocationPersonId", "RecieverAllocPersonEffecStartDate")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("PerformanceManagement.Models.HRAdmin.EvaluationHierarchy", b =>
                {
                    b.HasOne("PerformanceManagement.Models.People", "CreatedBy")
                        .WithMany("Createdby")
                        .HasForeignKey("CreatedById", "CreatedByEffectiveStartDate")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PerformanceManagement.Models.HRAdmin.Department", "Department")
                        .WithMany("EvaluationHierarchies")
                        .HasForeignKey("DepartmentId", "DepartmentEffectiveStartDate")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PerformanceManagement.Models.People", "LastUpdatedBy")
                        .WithMany("LastUpdatedBy")
                        .HasForeignKey("LastUpdatedById", "LastUpdatedByEffectiveStartDate")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PerformanceManagement.Models.HRAdmin.EvaluationHierarchy", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentEvaluationHierarchyId", "ParentEffectiveStartDate");

                    b.HasOne("PerformanceManagement.Models.People", "Supervisor")
                        .WithMany("EvaluationHierarchies")
                        .HasForeignKey("SupervisorId", "SupervisorEffectiveStartDate")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("PerformanceManagement.Models.HRAdmin.LikertDescribor", b =>
                {
                    b.HasOne("PerformanceManagement.Models.HRAdmin.LikertScale", "LikertScale")
                        .WithMany("LikertDescribors")
                        .HasForeignKey("LikertScaleId", "LikertScaleEffectiveStartDate")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("PerformanceManagement.Models.HRAdmin.PeriodDefinitoion", b =>
                {
                    b.HasOne("PerformanceManagement.Models.HRAdmin.EvaluationCoefficient", "EvaluationCoefficient")
                        .WithOne("PeriodDefinitoion")
                        .HasForeignKey("PerformanceManagement.Models.HRAdmin.PeriodDefinitoion", "EvaluationCoefficientId");

                    b.HasOne("PerformanceManagement.Models.HRAdmin.LikertScale", "LikertScoreWay")
                        .WithMany("periodDefinitoionLikertScoreWay")
                        .HasForeignKey("LikertScoreWayId", "LikertScoreWayEffectiveStartDate")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PerformanceManagement.Models.HRAdmin.LikertScale", "LikertWeightWayBehaiviour")
                        .WithMany("periodDefinitoionLikertBehaiviour")
                        .HasForeignKey("LikertWeightWayBehaiviourId", "LikertWeightWayBehaiviourEffectiveStartDate")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PerformanceManagement.Models.HRAdmin.LikertScale", "LikertWeightWayTask")
                        .WithMany("periodDefinitoionLikertTask")
                        .HasForeignKey("LikertWeightWayTaskId", "LikertWeightWayTaskEffectiveStartDate")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("PerformanceManagement.Models.HRAdmin.PersonPeriodEvaluationHierarchy", b =>
                {
                    b.HasOne("PerformanceManagement.Models.HRAdmin.PeriodDefinitoion", "periodDefinitoion")
                        .WithMany("PersonAndPeriods")
                        .HasForeignKey("PeriodDefinitoionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PerformanceManagement.Models.HRAdmin.EvaluationHierarchy", "evaluationHierarchy")
                        .WithMany("PersonAndPeriodAndEvaluationHierarchies")
                        .HasForeignKey("EvaluationHierarchyId", "EvaluationHierarchyEffectiveStartDate")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PerformanceManagement.Models.People", "people")
                        .WithMany("PersonAndPeriods")
                        .HasForeignKey("PeopleId", "PeopleEffectiveStartDate")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("PerformanceManagement.Models.HRAdmin.SectionPeriod", b =>
                {
                    b.HasOne("PerformanceManagement.Models.HRAdmin.PeriodDefinitoion", "periodDefinitoion")
                        .WithMany("SectionPeriods")
                        .HasForeignKey("PeriodDefinitoionId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("PerformanceManagement.Models.People", b =>
                {
                    b.HasOne("PerformanceManagement.Models.HRAdmin.EvaluationHierarchy", "evaluationHierarchy")
                        .WithMany("peopledepartmentHierarchy")
                        .HasForeignKey("EvaluationHierarchyID", "EvaluationHierarchyDate")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("PerformanceManagement.Models.Task", b =>
                {
                    b.HasOne("PerformanceManagement.Models.Task", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentTaskId");

                    b.HasOne("PerformanceManagement.Models.HRAdmin.PeriodDefinitoion", "PeriodDefinitoion")
                        .WithMany("Tasks")
                        .HasForeignKey("PeriodDefinitoionId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("PerformanceManagement.Models.ICTAdmin.ApplicationUser", b =>
                {
                    b.HasOne("PerformanceManagement.Models.People", "People")
                        .WithMany()
                        .HasForeignKey("PeopleId", "PeopleEffectiveStartDate");
                });
#pragma warning restore 612, 618
        }
    }
}
