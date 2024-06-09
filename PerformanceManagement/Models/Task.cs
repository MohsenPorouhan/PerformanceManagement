using PerformanceManagement.Models.Coacher;
using PerformanceManagement.Models.HRAdmin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string RoleId { get; set; }
        public int? Type { get; set; }
        public int ResourceType { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public int? ParentTaskId { get; set; }
        public virtual Task Parent { get; set; }
        public virtual ICollection<Task> Children { get; set; }
        public int? PeriodDefinitoionId { get; set; }
        public PeriodDefinitoion PeriodDefinitoion { get; set; }
        public int? EvaluationHierarchyId { get; set; }
        public DateTime? EvaluationHierarchyEffectiveStartDate { get; set; }
        public EvaluationHierarchy EvaluationHierarchy { get; set; }
        public virtual ICollection<Criteria> Criterias { get; set; }
        public virtual ICollection<Evaluation> Evaluations { get; set; }
        public virtual ICollection<PublicTask> PublicTasks { get; set; }
        public virtual ICollection<PublicTask> CorrespondentTasks { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public virtual ICollection<RelatedTaskWithSensibleEvent> RelatedTaskWithSensibleEvents { get; set; }

    }
}
