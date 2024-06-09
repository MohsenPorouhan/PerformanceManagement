using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class FinalizeCalculationConfig : IEntityTypeConfiguration<FinalizeCalculation>
    {
        public void Configure(EntityTypeBuilder<FinalizeCalculation> builder)
        {
            builder.HasKey(c => new { c.FinalizeCalculationId });
        }
    }
}
