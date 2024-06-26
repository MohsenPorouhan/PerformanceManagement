﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class EvaluationBehaviourCompetencyScoreConfig : IEntityTypeConfiguration<EvaluationBehaviourCompetencyScore>
    {
        public void Configure(EntityTypeBuilder<EvaluationBehaviourCompetencyScore> builder)
        {
            builder.HasKey(c => new { c.EvaluationBehaviourCompetencyScoreId });

        }
    }
}
