using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class TrainingNeedConfig : IEntityTypeConfiguration<TrainingNeed>
    {
        public void Configure(EntityTypeBuilder<TrainingNeed> builder)
        {
            builder.HasKey(c => new { c.TrainingNeedId });
        }
    }
}
