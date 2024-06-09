using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PerformanceManagement.Models.HRAdmin
{
    public class ExtendSectionPeriodWithPeopleConfig : IEntityTypeConfiguration<ExtendSectionPeriodWithPeople>
    {
        public void Configure(EntityTypeBuilder<ExtendSectionPeriodWithPeople> builder)
        {
            builder.HasKey(c => new { c.ExtendSectionPeriodWithPeopleId });
        }
    }
}
