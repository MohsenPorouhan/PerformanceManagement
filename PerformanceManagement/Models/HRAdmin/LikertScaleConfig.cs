using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class LikertScaleConfig : IEntityTypeConfiguration<LikertScale>
    {
        public void Configure(EntityTypeBuilder<LikertScale> builder)
        {
            builder.HasKey(c => new { c.LikertScaleId, c.EffectiveStartDate });

            builder.HasMany(c => c.LikertDescribors).WithOne(c => c.LikertScale).HasForeignKey(c => new { c.LikertScaleId, c.LikertScaleEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.periodDefinitoionLikertBehaiviour).WithOne(c => c.LikertWeightWayBehaiviour).HasForeignKey(c => new { c.LikertWeightWayBehaiviourId, c.LikertWeightWayBehaiviourEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.periodDefinitoionLikertTask).WithOne(c => c.LikertWeightWayTask).HasForeignKey(c => new { c.LikertWeightWayTaskId, c.LikertWeightWayTaskEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.periodDefinitoionLikertScoreWay).WithOne(c => c.LikertScoreWay).HasForeignKey(c => new { c.LikertScoreWayId, c.LikertScoreWayEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(c => c.Name).IsUnique();
        }
    }
}
