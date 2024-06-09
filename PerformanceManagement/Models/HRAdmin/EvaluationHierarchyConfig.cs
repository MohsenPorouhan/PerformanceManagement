using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class EvaluationHierarchyConfig : IEntityTypeConfiguration<EvaluationHierarchy>
    {
        public void Configure(EntityTypeBuilder<EvaluationHierarchy> builder)
        {
            builder.HasKey(c => new { c.EvaluationHierarchyId, c.EffectiveStartDate });

            builder.HasMany(c => c.PersonAndPeriodAndEvaluationHierarchies).WithOne(c => c.evaluationHierarchy).HasForeignKey(c => new { c.EvaluationHierarchyId, c.EvaluationHierarchyEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.ChartConfirmations).WithOne(c => c.EvaluationHierarchy).HasForeignKey(c => new { c.EvaluationHierarchyId, c.EvalHieEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.AllocatorEvaluationHierarchies).WithOne(c => c.AllocatorEvaluationHierarchy).HasForeignKey(c => new { c.AllocatorEvaluationHierarchyId, c.AllocatorEvalHieEffcStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.RecieverAllocationEvaluationHierarchies).WithOne(c => c.RecieverAllocationEvaluationHierarchy).HasForeignKey(c => new { c.RecieverAllocationEvaluationHierarchyId, c.RecieverAllocEvalHieEffcStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.AllocatorEvaluationBehaviouralHierarchies).WithOne(c => c.AllocatorEvaluationBehaviouralHierarchy).HasForeignKey(c => new { c.AllocatorEvaluationBehaviouralHierarchyId, c.AllocatorEvalBehavHieEffcStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.RecieverAllocationEvaluationBehaviouralHierarchies).WithOne(c => c.RecieverAllocationEvaluationBehaviouralHierarchy).HasForeignKey(c => new { c.RecieverAllocationEvaluationBehaviouralHierarchyId, c.RecieverAllocEvalBehavHieEffcStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.EvaluationBehaviouralParticipants).WithOne(c => c.ParticipantEvaluationBehaviouralHierarchy).HasForeignKey(c => new { c.ParticipantEvaluationBehaviouralHierarchyId, c.ParticipantEvalBehavHieEffcStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.EvaluationParticipants).WithOne(c => c.ParticipantEvaluationHierarchy).HasForeignKey(c => new { c.ParticipantEvaluationHierarchyId, c.ParticipantEvalHieEffcStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Tasks).WithOne(c => c.EvaluationHierarchy).HasForeignKey(c => new { c.EvaluationHierarchyId, c.EvaluationHierarchyEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.AllocatorFinalScoreCalculations).WithOne(c => c.AllocatorEvaluationHierarchy).HasForeignKey(c => new { c.AllocatorEvaluationHierarchyId, c.AllocatorEvalHieEffcStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.RecieverFinalScoreCalculations).WithOne(c => c.RecieverAllocationEvaluationHierarchy).HasForeignKey(c => new { c.RecieverAllocationEvaluationHierarchyId, c.RecieverAllocEvalHieEffcStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.AllocatorFinalScoreCompetencyCalculations).WithOne(c => c.AllocatorEvaluationHierarchy).HasForeignKey(c => new { c.AllocatorEvaluationHierarchyId, c.AllocatorEvalHieEffcStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.RecieverFinalScoreCompetencyCalculations).WithOne(c => c.RecieverAllocationEvaluationHierarchy).HasForeignKey(c => new { c.RecieverAllocationEvaluationHierarchyId, c.RecieverAllocEvalHieEffcStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.SensibleEvents).WithOne(c => c.PersonDepartment).HasForeignKey(c => new { c.PersonDepartmentId, c.DepartmentEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.EmployeeSensibleEvent).WithOne(c => c.EmployeeDepartment).HasForeignKey(c => new { c.EmployeeDepartmentId, c.DepartmentEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.ExtendSectionPeriodWithPeople).WithOne(c => c.EvaluationHierarchy).HasForeignKey(c => new { c.EvaluationHierarchyId, c.EvaluationHierarchyEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Protests).WithOne(c => c.ProtesterDepartment).HasForeignKey(c => new { c.ProtesterDepartmentId, c.ProtesterDepartmnetEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Addressees).WithOne(c => c.CoacherDepartment).HasForeignKey(c => new { c.CoacherDepartmentId, c.CoacherDepartmnetEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.ProtestResponses).WithOne(c => c.PersonDepartment).HasForeignKey(c => new { c.PersonDepartmentId, c.PersonDepartmnetEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.ChartInfos).WithOne(c => c.EvaluationHierarchy).HasForeignKey(c => new { c.EvaluationHierarchyId, c.EvalEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

        }

    }
}
