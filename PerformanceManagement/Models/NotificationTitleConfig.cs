using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class NotificationTitleConfig : IEntityTypeConfiguration<NotificationTitle>
    {
        public void Configure(EntityTypeBuilder<NotificationTitle> builder)
        {
            builder.HasKey(c => new { c.NotificationTitleId });
            builder.HasMany(c => c.Notifications).WithOne(c => c.NotificationTitle).HasForeignKey(c => new { c.NotificationTitleId }).OnDelete(DeleteBehavior.Restrict);
            builder.HasData(new NotificationTitle
            {
                NotificationTitleId = 1,
                Title = "تغییر وضعیت تفاهم وظایف",
                CreatedDate = DateTime.Now
            }, new NotificationTitle
            {
                NotificationTitleId = 2,
                Title = "تغییر وضعیت تفاهم شایستگی ها",
                CreatedDate = DateTime.Now
            }, new NotificationTitle
            {
                NotificationTitleId = 3,
                Title = "تعیین وضعیت سایرارزیاب وظایف",
                CreatedDate = DateTime.Now
            }, new NotificationTitle
            {
                NotificationTitleId = 4,
                Title = "تعیین وضعیت سایر ارزیاب شایستگی رفتاری",
                CreatedDate = DateTime.Now
            });
        }
    }
}
