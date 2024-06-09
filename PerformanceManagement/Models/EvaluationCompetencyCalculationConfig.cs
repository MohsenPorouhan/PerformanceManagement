using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PerformanceManagement.Models
{
    public class EvaluationCompetencyCalculationConfig : IEntityTypeConfiguration<EvaluationCompetencyCalculation>
    {
        public void Configure(EntityTypeBuilder<EvaluationCompetencyCalculation> builder)
        {
            builder.HasKey(c => new { c.EvaluationCompetencyCalculationId });

            //builder.HasOne(e => e.Parent).WithMany(e => e.Children).HasForeignKey(e => new { e.ParentTaskId });
        }
    }
}
