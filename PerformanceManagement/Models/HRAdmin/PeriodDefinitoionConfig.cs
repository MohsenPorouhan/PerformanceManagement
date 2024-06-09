using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class PeriodDefinitoionConfig : IEntityTypeConfiguration<PeriodDefinitoion>
    {
        public void Configure(EntityTypeBuilder<PeriodDefinitoion> builder)
        {
            builder.HasMany(c => c.SectionPeriods).WithOne(c => c.periodDefinitoion).HasForeignKey(c => new { c.PeriodDefinitoionId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.PersonAndPeriods).WithOne(c => c.periodDefinitoion).HasForeignKey(c => new { c.PeriodDefinitoionId }).OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(c => c.PeriodCode).IsUnique();
            builder.HasIndex(c => c.PeriodTitle).IsUnique();

            builder.HasMany(c => c.ChartConfirmations).WithOne(c => c.PeriodDefinitoion).HasForeignKey(c => new { c.PeriodDefinitionId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Tasks).WithOne(c => c.PeriodDefinitoion).HasForeignKey(c => new { c.PeriodDefinitoionId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Evaluations).WithOne(c => c.PeriodDefinitoion).HasForeignKey(c => new { c.PeriodDefinitoionId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.EvaluationBehaviouralCompetencies).WithOne(c => c.PeriodDefinitoion).HasForeignKey(c => new { c.PeriodDefinitoionId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.FinalScoreCalculations).WithOne(c => c.PeriodDefinitoion).HasForeignKey(c => new { c.PeriodDefinitoionId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.FinalScoreCompetencyCalculations).WithOne(c => c.PeriodDefinitoion).HasForeignKey(c => new { c.PeriodDefinitoionId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.FinalizeCalculations).WithOne(c => c.PeriodDefinitoion).HasForeignKey(c => new { c.PeriodDefinitoionId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.SensibleEvents).WithOne(c => c.PeriodDefinition).HasForeignKey(c => new { c.PeriodDefinitionId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.MultipleTaskCoacherOfEmployeeFinalCalcs).WithOne(c => c.PeriodDefinition).HasForeignKey(c => new { c.PeriodDefinitionId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.MultipleCompetencyCoacherOfEmployeeFinalCalcs).WithOne(c => c.PeriodDefinition).HasForeignKey(c => new { c.PeriodDefinitionId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Protests).WithOne(c => c.PeriodDefinition).HasForeignKey(c => new { c.PeriodDefinitionId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.ScoreSchedules).WithOne(c => c.PeriodDefinitoion).HasForeignKey(c => new { c.PeriodDefinitionId }).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
