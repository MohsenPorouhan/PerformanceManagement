using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class EvaluationParticipantConfig : IEntityTypeConfiguration<EvaluationParticipant>
    {
        public void Configure(EntityTypeBuilder<EvaluationParticipant> builder)
        {
            builder.HasKey(c => new { c.EvaluationParticipantId });
        }
    }
}
