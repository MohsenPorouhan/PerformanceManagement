using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceManagement.Models.HRAdmin;

namespace PerformanceManagement.Models
{
    public class MultipleTaskCoacherOfEmployeeFinalCalcConfig : IEntityTypeConfiguration<MultipleTaskCoacherOfEmployeeFinalCalc>
    {
        public void Configure(EntityTypeBuilder<MultipleTaskCoacherOfEmployeeFinalCalc> builder)
        {
            builder.HasKey(c => new { c.MultipleTaskCoacherOfEmployeeFinalCalcId });
        }
    }
}
