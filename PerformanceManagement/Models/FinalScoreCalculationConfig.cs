using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class FinalScoreCalculationConfig : IEntityTypeConfiguration<FinalScoreCalculation>
    {
        public void Configure(EntityTypeBuilder<FinalScoreCalculation> builder)
        {
            builder.HasKey(c => new { c.FinalScoreCalculationId });
        }
    }
}
