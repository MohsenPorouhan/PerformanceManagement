using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class ChartInfoConfig : IEntityTypeConfiguration<ChartInfo>
    {
        public void Configure(EntityTypeBuilder<ChartInfo> builder)
        {
            builder.HasKey(c => new { c.ChartInfoId });
        }

    }
}
