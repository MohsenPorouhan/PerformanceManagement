using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class EvaluationCoefficientConfig : IEntityTypeConfiguration<EvaluationCoefficient>
    {
        public void Configure(EntityTypeBuilder<EvaluationCoefficient> builder)
        {
            builder.HasKey(c => new { c.EvaluationCoefficientId });
            builder.HasOne(c => c.PeriodDefinitoion).WithOne(c => c.EvaluationCoefficient).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
