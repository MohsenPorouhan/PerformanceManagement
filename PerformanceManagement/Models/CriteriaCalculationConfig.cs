using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class CriteriaCalculationConfig : IEntityTypeConfiguration<CriteriaCalculation>
    {
        public void Configure(EntityTypeBuilder<CriteriaCalculation> builder)
        {
            builder.HasKey(c => new { c.CriteriaCalculationId });
        }
    }
}
