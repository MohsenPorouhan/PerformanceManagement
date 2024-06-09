using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class ScoreScheduleConfig : IEntityTypeConfiguration<ScoreSchedule>
    {
        public void Configure(EntityTypeBuilder<ScoreSchedule> builder)
        {
            builder.HasKey(c => new { c.ScoreScheduleId });
        }
    }
}
