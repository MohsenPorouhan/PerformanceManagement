using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class LikertDescriborConfig : IEntityTypeConfiguration<LikertDescribor>
    {
        public void Configure(EntityTypeBuilder<LikertDescribor> builder)
        {
            builder.HasKey(c => new { c.LikertDescriborId, c.EffectiveStartDate });
        }

    }
}
