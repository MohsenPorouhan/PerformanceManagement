using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class EvaluationConfig : IEntityTypeConfiguration<Evaluation>
    {
        public void Configure(EntityTypeBuilder<Evaluation> builder)
        {
            builder.HasKey(c => new { c.EvaluationId });
            builder.HasMany(c => c.CriteriaWeights).WithOne(c => c.Evaluation).HasForeignKey(c => new { c.EvaluationId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.EvaluationScores).WithOne(c => c.Evaluation).HasForeignKey(c => new { c.EvaluationId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.EvaluationParticipants).WithOne(c => c.Evaluation).HasForeignKey(c => new { c.EvaluationId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.TrainingNeeds).WithOne(c => c.Evaluation).HasForeignKey(c => new { c.EvaluationId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.EvaluationCalculations).WithOne(c => c.Evaluation).HasForeignKey(c => new { c.EvaluationId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.CriteriaCalculations).WithOne(c => c.Evaluation).HasForeignKey(c => new { c.EvaluationId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.RelatedTaskWithSensibleEvents).WithOne(c => c.Evaluation).HasForeignKey(c => new { c.EvaluationId }).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
