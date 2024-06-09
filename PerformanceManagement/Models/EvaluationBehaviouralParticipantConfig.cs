﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class EvaluationBehaviouralParticipantConfig : IEntityTypeConfiguration<EvaluationBehaviouralParticipant>
    {
        public void Configure(EntityTypeBuilder<EvaluationBehaviouralParticipant> builder)
        {
            builder.HasKey(c => new { c.EvaluationBehaviouralParticipantId });
        }
    }
}
