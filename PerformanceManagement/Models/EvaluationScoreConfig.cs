using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class EvaluationScoreConfig : IEntityTypeConfiguration<EvaluationScore>
    {
        public void Configure(EntityTypeBuilder<EvaluationScore> builder)
        {
            builder.HasKey(c => new { c.EvaluationScoreId });
        }
    }
}
