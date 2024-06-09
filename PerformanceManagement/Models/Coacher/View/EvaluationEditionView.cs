using PerformanceManagement.Models.HRAdmin.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.Coacher.View
{
    [NotMapped]
    public class EvaluationEditionView
    {
        public int? TaskId { get; set; }
        public string TaskTitle { get; set; }
        public int? ParentTaskId { get; set; }
        public int? EvaluationId { get; set; }
        public int? ParticipantId { get; set; }
        public int? ParticipantDepartmentId { get; set; }
        public int? EvaluationParticipantId { get; set; }
        public int[] CriteriaDeletion { get; set; }
        public int[] TrainingNeedDeletion { get; set; }
        public string TrainingNeedInsertion { get; set; }
        public ICollection<CriteriaView> CriteriaViews { get; set; }
        public ICollection<TrainingNeed> TrainingNeeds { get; set; }
        public ICollection<CriteriaInsertionView> CriteriaInsertionViews { get; set; }
    }
}
