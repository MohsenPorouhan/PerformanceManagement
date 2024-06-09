using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.Coacher
{
    public class CoacherTruthConfig : IEntityTypeConfiguration<CoacherTruth>
    {
        public void Configure(EntityTypeBuilder<CoacherTruth> builder)
        {
            builder.HasKey(c => new { c.CoacherTruthId });
        }
    }
}
