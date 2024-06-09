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
    public class EvaluationBehaviouralParticipant
    {
        public int EvaluationBehaviouralParticipantId { get; set; }
        public int? Score { get; set; }
        public bool? Confirmation { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public int EvaluationBehaviouralCompetencyId { get; set; }
        public EvaluationBehaviouralCompetency EvaluationBehaviouralCompetency { get; set; }
        public int ParticipantId { get; set; }
        public DateTime? PeopleEffectiveStartDate { get; set; }
        public People Participant { get; set; }
        public DateTime? ParticipantEvalBehavHieEffcStartDate { get; set; }
        public int ParticipantEvaluationBehaviouralHierarchyId { get; set; }
        public EvaluationHierarchy ParticipantEvaluationBehaviouralHierarchy { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
