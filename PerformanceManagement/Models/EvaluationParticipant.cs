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
    public class EvaluationParticipant
    {
        public int EvaluationParticipantId { get; set; }
        public int? Score { get; set; }
        public bool? Confirmation { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public int EvaluationId { get; set; }
        public Evaluation Evaluation { get; set; }
        public int ParticipantId { get; set; }
        public DateTime? PeopleEffectiveStartDate { get; set; }
        public People Participant { get; set; }
        public DateTime? ParticipantEvalHieEffcStartDate { get; set; }
        public int ParticipantEvaluationHierarchyId { get; set; }
        public EvaluationHierarchy ParticipantEvaluationHierarchy { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
