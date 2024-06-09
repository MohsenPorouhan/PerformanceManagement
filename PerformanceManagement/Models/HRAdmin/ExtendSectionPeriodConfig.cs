using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class ExtendSectionPeriodConfig : IEntityTypeConfiguration<ExtendSectionPeriod>
    {
        public void Configure(EntityTypeBuilder<ExtendSectionPeriod> builder)
        {
            builder.HasKey(c => new { c.ExtendSectionPeriodId });

            builder.HasMany(c => c.ExtendSectionPeriodWithPeople).WithOne(c => c.ExtendSectionPeriod).HasForeignKey(c => new { c.ExtendSectionPeriodId }).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(c => c.ExtendScoreSchedules).WithOne(c => c.ExtendSectionPeriod).HasForeignKey(c => new { c.ExtendSectionPeriodId }).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
