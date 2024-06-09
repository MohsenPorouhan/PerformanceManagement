using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class CriteriaWeightConfig : IEntityTypeConfiguration<CriteriaWeight>
    {
        public void Configure(EntityTypeBuilder<CriteriaWeight> builder)
        {
            builder.HasKey(c => new { c.CriteriaWeightId });

            builder.HasMany(c => c.CriteriaWeightScores).WithOne(c => c.CriteriaWeight).HasForeignKey(c => new { c.CriteriaWeightId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.CriteriaCalculations).WithOne(c => c.CriteriaWeight).HasForeignKey(c => new { c.CriteriaWeightId }).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
