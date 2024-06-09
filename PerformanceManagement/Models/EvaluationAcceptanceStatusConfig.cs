using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class EvaluationAcceptanceStatusConfig : IEntityTypeConfiguration<EvaluationAcceptanceStatus>
    {
        public void Configure(EntityTypeBuilder<EvaluationAcceptanceStatus> builder)
        {
            builder.HasKey(c => new { c.EvaluationAcceptanceStatusId });

            builder.HasMany(c => c.Evaluations).WithOne(c => c.EvaluationAcceptanceStatus).HasForeignKey(c => new { c.EvaluationAcceptanceStatusId }).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(c => c.EvaluationBehaviouralCompetencies).WithOne(c => c.EvaluationAcceptanceStatus).HasForeignKey(c => new { c.EvaluationAcceptanceStatusId }).OnDelete(DeleteBehavior.Restrict);
            builder.HasData(
                new EvaluationAcceptanceStatus
                {
                    EvaluationAcceptanceStatusId = 1,
                    Title = "تفاهم",
                    CreatedDate = DateTime.Now
                },
                new EvaluationAcceptanceStatus
                {
                    EvaluationAcceptanceStatusId = 2,
                    Title = "ابلاغی",
                    CreatedDate = DateTime.Now
                },
                new EvaluationAcceptanceStatus
                {
                    EvaluationAcceptanceStatusId = 3,
                    Title = "صرف نظر",
                    CreatedDate = DateTime.Now
                },
                new EvaluationAcceptanceStatus
                {
                    EvaluationAcceptanceStatusId = 4,
                    Title = "نامشخص",
                    CreatedDate = DateTime.Now
                });
        }
    }
}
