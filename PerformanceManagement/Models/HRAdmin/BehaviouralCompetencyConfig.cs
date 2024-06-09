using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class BehaviouralCompetencyConfig : IEntityTypeConfiguration<BehaviouralCompetency>
    {
        public void Configure(EntityTypeBuilder<BehaviouralCompetency> builder)
        {
            builder.HasKey(c => new { c.BehaviouralCompetencyId });

            builder.HasMany(c => c.Truths).WithOne(c => c.BehaviouralCompetency).HasForeignKey(c => new { c.BehaviouralCompetencyId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.CorrespondentJobs).WithOne(c => c.BehaviouralCompetency).HasForeignKey(c => new { c.BehaviouralCompetencyId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.EvaluationBehaviouralCompetencies).WithOne(c => c.BehaviouralCompetency).HasForeignKey(c => new { c.BehaviouralCompetencyId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.RelatedCompetencyWithSensibleEvents).WithOne(c => c.BehaviouralCompetency).HasForeignKey(c => new { c.BehaviouralCompetencyId }).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
