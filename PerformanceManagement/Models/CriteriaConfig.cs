using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class CriteriaConfig : IEntityTypeConfiguration<Criteria>
    {
        public void Configure(EntityTypeBuilder<Criteria> builder)
        {
            builder.HasKey(c => new { c.CriteriaId });

            builder.HasMany(c => c.CriteriaWeights).WithOne(c => c.Criteria).HasForeignKey(c => new { c.CriteriaId }).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
