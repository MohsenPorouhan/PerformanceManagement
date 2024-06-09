using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.View
{
    [NotMapped]
    public class EditCompetencyAssignmentView
    {
        public int EvaluationBehaviouralCompetencyId { get; set; }
        public int BehaviouralCompetencyWeight { get; set; }
        public string Title { get; set; }
        public bool HasParticipant { get; set; }
        public int? EvaluationBehaviouralParticipantId { get; set; }
        public int? ParticipantEvaluationBehaviouralHierarchyId { get; set; }
        public int? ParticipantId { get; set; }
        public int PeriodDefinitionId { get; set; }
        public IEnumerable<ParticipantView> EvaluationCompetencyParticipants { get; set; }
    }
}
