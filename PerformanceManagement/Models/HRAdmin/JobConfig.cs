using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class JobConfig : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasKey(c => new { c.JobId });

            builder.HasMany(c => c.CorrespondentJobs).WithOne(c => c.Job).HasForeignKey(c => new { c.JobId }).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(c => c.Peoples).WithOne(c => c.Job).HasForeignKey(c => new { c.JobId }).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
