using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class TaskConfig : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.HasKey(c => new { c.TaskId });

            builder.HasOne(e => e.Parent).WithMany(e => e.Children).HasForeignKey(e => new { e.ParentTaskId });

            builder.HasMany(c => c.Criterias).WithOne(c => c.Task).HasForeignKey(c => new { c.TaskId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Evaluations).WithOne(c => c.Task).HasForeignKey(c => new { c.TaskId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.PublicTasks).WithOne(c => c.Task).HasForeignKey(c => new { c.TaskId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.CorrespondentTasks).WithOne(c => c.CorrespondentTask).HasForeignKey(c => new { c.CorrespondentTaskId }).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.RelatedTaskWithSensibleEvents).WithOne(c => c.Task).HasForeignKey(c => new { c.TaskId }).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
