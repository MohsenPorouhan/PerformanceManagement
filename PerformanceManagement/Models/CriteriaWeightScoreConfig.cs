using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class CriteriaWeightScoreConfig : IEntityTypeConfiguration<CriteriaWeightScore>
    {
        public void Configure(EntityTypeBuilder<CriteriaWeightScore> builder)
        {
            builder.HasKey(c => new { c.CriteriaWeightScoreId });
        }
    }
}
