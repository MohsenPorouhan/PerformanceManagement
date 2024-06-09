using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class ProtestConfig : IEntityTypeConfiguration<Protest>
    {
        public void Configure(EntityTypeBuilder<Protest> builder)
        {
            builder.HasKey(c => new { c.ProtestId });

            builder.HasMany(c => c.Addressees).WithOne(c => c.Protest).HasForeignKey(c => new { c.ProtestId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.ProtestResponses).WithOne(c => c.Protest).HasForeignKey(c => new { c.ProtestId }).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
