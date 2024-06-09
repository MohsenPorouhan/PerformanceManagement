using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class SensibleEventConfig : IEntityTypeConfiguration<SensibleEvent>
    {
        public void Configure(EntityTypeBuilder<SensibleEvent> builder)
        {
            builder.HasKey(c => new { c.SensibleEventId });

            builder.HasMany(c => c.RelatedCompetencyWithSensibleEvents).WithOne(c => c.SensibleEvent).HasForeignKey(c => new { c.SensibleEventId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.RelatedTaskWithSensibleEvents).WithOne(c => c.SensibleEvent).HasForeignKey(c => new { c.SensibleEventId }).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
