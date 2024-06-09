using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class ExtendScoreScheduleConfig : IEntityTypeConfiguration<ExtendScoreSchedule>
    {
        public void Configure(EntityTypeBuilder<ExtendScoreSchedule> builder)
        {
            builder.HasKey(c => new { c.ExtendScoreScheduleId });
        }
    }
}
