using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class PeopleConfig : IEntityTypeConfiguration<People>
    {
        public void Configure(EntityTypeBuilder<People> builder)
        {
            builder.HasKey(c => new { c.PeopleId, c.EffectiveStartDate });

            builder.HasMany(c => c.PersonAndPeriods).WithOne(c => c.people).HasForeignKey(c => new { c.PeopleId, c.PeopleEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.ChartConfirmations).WithOne(c => c.Supervisor).HasForeignKey(c => new { c.SupervisorId, c.SupervisorEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Allocators).WithOne(c => c.Allocator).HasForeignKey(c => new { c.AllocatorPersonId, c.AllocatorPersonEffecStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.RecieversAllocation).WithOne(c => c.RecieverAllocation).HasForeignKey(c => new { c.RecieverAllocationPersonId, c.RecieverAllocPersonEffecStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.EvaluationScores).WithOne(c => c.Coacher).HasForeignKey(c => new { c.CoacherId, c.CoacherEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.CriteriaWeightScores).WithOne(c => c.Coacher).HasForeignKey(c => new { c.CoacherId, c.CoacherEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.AllocatorsEvaluationBehavioural).WithOne(c => c.Allocator).HasForeignKey(c => new { c.AllocatorPersonId, c.AllocatorPersonEffecStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.RecieversAllocationEvaluationBehavioural).WithOne(c => c.RecieverAllocation).HasForeignKey(c => new { c.RecieverAllocationPersonId, c.RecieverAllocPersonEffecStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.EvaluationBehaviourCompetencyScores).WithOne(c => c.Coacher).HasForeignKey(c => new { c.CoacherId, c.CoacherEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.EvaluationBehaviouralParticipants).WithOne(c => c.Participant).HasForeignKey(c => new { c.ParticipantId, c.PeopleEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.EvaluationParticipants).WithOne(c => c.Participant).HasForeignKey(c => new { c.ParticipantId, c.PeopleEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.AllocatorFinalScoreCalculations).WithOne(c => c.Allocator).HasForeignKey(c => new { c.AllocatorPersonId, c.AllocatorPersonEffecStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.RecieverFinalScoreCalculations).WithOne(c => c.RecieverAllocation).HasForeignKey(c => new { c.RecieverAllocationPersonId, c.RecieverAllocPersonEffecStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.AllocatorFinalScoreCompetencyCalculations).WithOne(c => c.Allocator).HasForeignKey(c => new { c.AllocatorPersonId, c.AllocatorPersonEffecStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.RecieverFinalScoreCompetencyCalculations).WithOne(c => c.RecieverAllocation).HasForeignKey(c => new { c.RecieverAllocationPersonId, c.RecieverAllocPersonEffecStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.FinalizeCalculations).WithOne(c => c.Coacher).HasForeignKey(c => new { c.CocherId, c.CoacherEffecStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.SensibleEvents).WithOne(c => c.Person).HasForeignKey(c => new { c.PersonId, c.PersonEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.EmployeeSensibleEvents).WithOne(c => c.Employee).HasForeignKey(c => new { c.EmployeeId, c.EmployeeEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.ExtendSectionPeriodWithPeople).WithOne(c => c.People).HasForeignKey(c => new { c.PeopleId, c.PeopleEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.MultipleTaskCoacherOfEmployeeFinalCalcs).WithOne(c => c.People).HasForeignKey(c => new { c.EmployeeId, c.EmployeeEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.MultipleCompetencyCoacherOfEmployeeFinalCalcs).WithOne(c => c.People).HasForeignKey(c => new { c.EmployeeId, c.EmployeeEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Protests).WithOne(c => c.Protester).HasForeignKey(c => new { c.ProtesterId, c.ProtesterEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Addressees).WithOne(c => c.Coacher).HasForeignKey(c => new { c.CoacherId, c.CoacherEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.ProtestResponses).WithOne(c => c.Person).HasForeignKey(c => new { c.PersonId, c.PersonEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.ChartInfos).WithOne(c => c.People).HasForeignKey(c => new { c.PeopleId, c.PeopleEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
