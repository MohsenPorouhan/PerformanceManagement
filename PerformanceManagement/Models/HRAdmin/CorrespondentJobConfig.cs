using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class CorrespondentJobConfig : IEntityTypeConfiguration<CorrespondentJob>
    {
        public void Configure(EntityTypeBuilder<CorrespondentJob> builder)
        {
            builder.HasKey(c => new { c.CorrespondentJobId });
        }
    }
}
