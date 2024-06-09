using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class EvaluationCalculationConfig : IEntityTypeConfiguration<EvaluationCalculation>
    {
        public void Configure(EntityTypeBuilder<EvaluationCalculation> builder)
        {
            builder.HasKey(c => new { c.EvaluationCalculationId });
        }
    }
}
