using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceManagement.Models.HRAdmin;

namespace PerformanceManagement.Models
{
    public class MultipleCompetencyCoacherOfEmployeeFinalCalcConfig : IEntityTypeConfiguration<MultipleCompetencyCoacherOfEmployeeFinalCalc>
    {
        public void Configure(EntityTypeBuilder<MultipleCompetencyCoacherOfEmployeeFinalCalc> builder)
        {
            builder.HasKey(c => new { c.MultipleCompetencyCoacherOfEmployeeFinalCalcId });
        }
    }
}
