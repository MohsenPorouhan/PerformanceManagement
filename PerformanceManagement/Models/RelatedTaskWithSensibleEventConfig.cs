using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class RelatedTaskWithSensibleEventConfig : IEntityTypeConfiguration<RelatedTaskWithSensibleEvent>
    {
        public void Configure(EntityTypeBuilder<RelatedTaskWithSensibleEvent> builder)
        {
            builder.HasKey(c => new { c.RelatedTaskWithSensibleEventId });
        }
    }
}
