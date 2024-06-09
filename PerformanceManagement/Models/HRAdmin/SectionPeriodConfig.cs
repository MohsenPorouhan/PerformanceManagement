using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class SectionPeriodConfig : IEntityTypeConfiguration<SectionPeriod>
    {
        public void Configure(EntityTypeBuilder<SectionPeriod> builder)
        {
            builder.HasKey(c => new { c.SectionPeriodId });

            builder.HasMany(c => c.ExtendSectionPeriods).WithOne(c => c.SectionPeriod).HasForeignKey(c => new { c.SectionPeriodId }).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
