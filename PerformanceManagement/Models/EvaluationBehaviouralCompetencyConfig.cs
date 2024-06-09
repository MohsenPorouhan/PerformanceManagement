using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PerformanceManagement.Models
{
    public class EvaluationBehaviouralCompetencyConfig : IEntityTypeConfiguration<EvaluationBehaviouralCompetency>
    {
        public void Configure(EntityTypeBuilder<EvaluationBehaviouralCompetency> builder)
        {
            builder.HasKey(c => new { c.EvaluationBehaviouralCompetencyId });

            //builder.HasOne(e => e.Parent).WithMany(e => e.Children).HasForeignKey(e => new { e.ParentTaskId });

            builder.HasMany(c => c.EvaluationBehaviourCompetencyScores).WithOne(c => c.EvaluationBehaviouralCompetency).HasForeignKey(c => new { c.EvaluationBehaviouralCompetencyId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.EvaluationBehaviouralParticipants).WithOne(c => c.EvaluationBehaviouralCompetency).HasForeignKey(c => new { c.EvaluationBehaviouralCompetencyId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.EvaluationCompetencyCalculations).WithOne(c => c.EvaluationBehaviouralCompetency).HasForeignKey(c => new { c.EvaluationBehaviouralCompetencyId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.RelatedCompetencyWithSensibleEvents).WithOne(c => c.EvaluationBehaviouralCompetency).HasForeignKey(c => new { c.EvaluationBehaviouralCompetencyId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.CoacherTruths).WithOne(c => c.EvaluationBehaviouralCompetency).HasForeignKey(c => new { c.EvaluationBehaviouralCompetencyId }).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
