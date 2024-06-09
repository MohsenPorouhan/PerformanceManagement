using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class FinalScoreCompetencyCalculationConfig : IEntityTypeConfiguration<FinalScoreCompetencyCalculation>
    {
        public void Configure(EntityTypeBuilder<FinalScoreCompetencyCalculation> builder)
        {
            builder.HasKey(c => new { c.FinalScoreCompetencyCalculationId });
        }
    }
}
