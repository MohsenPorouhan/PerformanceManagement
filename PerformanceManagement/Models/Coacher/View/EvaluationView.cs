using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.Coacher.View
{
    [NotMapped]
    public class EvaluationView
    {
        public int PeriodDefinitionId { get; set; }
        public int? AllocatorDepartmentId { get; set; }
        public int allocatorPersonId { get; set; }
        public string TaskTitle { get; set; }
        public int ResourceType { get; set; }
        public int TaskId { get; set; }
        public int? ParentTaskId { get; set; }
        public bool? ParticipantConfirmation { get; set; }
        public int? HasParticipant { get; set; }
        public int EvaluationId { get; set; }
        public int? ParticipantId { get; set; }
        public int? ParticipantDepartmentId { get; set; }
        public int? EvaluationParticipantId { get; set; }
        public IEnumerable<ParticipantView> EvaluationParticipants { get; set; }
        public ICollection<Criteria> Criterias { get; set; }
        public ICollection<TrainingNeed> TrainingNeeds { get; set; }

    }
}
