using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class TruthConfig : IEntityTypeConfiguration<Truth>
    {
        public void Configure(EntityTypeBuilder<Truth> builder)
        {
            builder.HasKey(c => new { c.TruthId });
        }
    }
}
