using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class RelatedCompetencyWithSensibleEventConfig : IEntityTypeConfiguration<RelatedCompetencyWithSensibleEvent>
    {
        public void Configure(EntityTypeBuilder<RelatedCompetencyWithSensibleEvent> builder)
        {
            builder.HasKey(c => new { c.RelatedCompetencyWithSensibleEventId });
        }
    }
}
