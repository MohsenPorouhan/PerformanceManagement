using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceManagement.Models.Coacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class ChartConfirmationConfig : IEntityTypeConfiguration<ChartConfirmation>
    {
        public void Configure(EntityTypeBuilder<ChartConfirmation> builder)
        {
            builder.HasKey(c => new { c.ChartConfirmationId, c.EvaluationHierarchyId, c.PeriodDefinitionId });
        }
    }
}
