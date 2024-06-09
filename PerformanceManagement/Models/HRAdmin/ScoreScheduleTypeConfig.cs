using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class ScoreScheduleTypeConfig : IEntityTypeConfiguration<ScoreScheduleType>
    {
        public void Configure(EntityTypeBuilder<ScoreScheduleType> builder)
        {
            builder.HasKey(c => new { c.ScoreScheduleTypeId });
            builder.HasMany(c => c.ScoreSchedules).WithOne(c=> c.ScoreScheduleType).HasForeignKey(c => new { c.ScoreScheduleTypeId }).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(c => c.ExtendScoreSchedules).WithOne(c => c.ScoreScheduleType).HasForeignKey(c => new { c.ScoreScheduleTypeId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasData(new ScoreScheduleType
            {
                ScoreScheduleTypeId = 1,
                Title = "خود ارزیابی",
                CreatedDate = DateTime.Now
            }, new ScoreScheduleType
            {
                ScoreScheduleTypeId = 2,
                Title = "سایرارزیاب",
                CreatedDate = DateTime.Now
            }, new ScoreScheduleType
            {
                ScoreScheduleTypeId = 3,
                Title = "مربی سطح 1 و بالاتر از سطح 2",
                CreatedDate = DateTime.Now
            }, new ScoreScheduleType
            {
                ScoreScheduleTypeId = 4,
                Title = "مربی سطح 2",
                CreatedDate = DateTime.Now
            });
        }
    }
}
