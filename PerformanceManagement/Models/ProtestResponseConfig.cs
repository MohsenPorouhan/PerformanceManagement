using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class ProtestResponseConfig : IEntityTypeConfiguration<ProtestResponse>
    {
        public void Configure(EntityTypeBuilder<ProtestResponse> builder)
        {
            builder.HasKey(c => new { c.ProtestResponseId });
        }
    }
}
