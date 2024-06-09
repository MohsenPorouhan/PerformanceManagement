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
    [Migration("20191201131117_add-06")]
    partial class add06
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

            modelBuilder.Entity("PerformanceManagement.Models.HRAdmin.PeriodDefinitoion", b =>
                {
                    b.Property<int>("PeriodDefinitoionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateFrom");

                    b.Property<DateTime>("DateTo");

                    b.Property<int>("PeriodCode");

                    b.Property<string>("PeriodTitle");

                    b.HasKey("PeriodDefinitoionId");

                    b.ToTable("PeriodDefinitoion");
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
