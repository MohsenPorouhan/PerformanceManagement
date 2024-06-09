using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.View
{
    [NotMapped]
    public class EditPublicTaskAssignmentView
    {
        public int EvaluationId { get; set; }
        public int TaskWeight { get; set; }
        public string Title { get; set; }
        public int? EvaluationParticipantId { get; set; }
        public int? ParticipantEvaluationHierarchyId { get; set; }
        public int? ParticipantId { get; set; }
        public int PeriodDefinitoionId { get; set; }
        public IEnumerable<ParticipantView> EvaluationParticipants { get; set; }
    }
}
