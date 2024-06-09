using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class PublicTaskConfig : IEntityTypeConfiguration<PublicTask>
    {
        public void Configure(EntityTypeBuilder<PublicTask> builder)
        {
            builder.HasKey(c => new { c.PublicTaskId });
        }
    }
}
