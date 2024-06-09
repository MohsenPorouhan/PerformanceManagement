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
    public class EvaluationBehaviouralCompetency
    {
        public int EvaluationBehaviouralCompetencyId { get; set; }
        public int BehaviouralCompetencyId { get; set; }
        public BehaviouralCompetency BehaviouralCompetency { get; set; }
        public int? BehaviouralCompetencyWeight { get; set; }
        public DateTime? AllocatorEvalBehavHieEffcStartDate { get; set; }
        public int? AllocatorEvaluationBehaviouralHierarchyId { get; set; }
        public EvaluationHierarchy AllocatorEvaluationBehaviouralHierarchy { get; set; }
        public DateTime? RecieverAllocEvalBehavHieEffcStartDate { get; set; }
        public int RecieverAllocationEvaluationBehaviouralHierarchyId { get; set; }
        public EvaluationHierarchy RecieverAllocationEvaluationBehaviouralHierarchy { get; set; }
        public DateTime? AllocatorPersonEffecStartDate { get; set; }
        public int AllocatorPersonId { get; set; }
        public People Allocator { get; set; }
        public string AllocatorRoleId { get; set; }
        public DateTime? RecieverAllocPersonEffecStartDate { get; set; }
        public int RecieverAllocationPersonId { get; set; }
        public People RecieverAllocation { get; set; }
        public int PeriodDefinitoionId { get; set; }
        public PeriodDefinitoion PeriodDefinitoion { get; set; }
        public int EvaluationAcceptanceStatusId { get; set; }
        public string RefutationCause { get; set; }
        public EvaluationAcceptanceStatus EvaluationAcceptanceStatus { get; set; }
        public virtual ICollection<EvaluationBehaviourCompetencyScore> EvaluationBehaviourCompetencyScores { get; set; }
        public virtual ICollection<EvaluationBehaviouralParticipant> EvaluationBehaviouralParticipants { get; set; }
        public virtual ICollection<EvaluationCompetencyCalculation> EvaluationCompetencyCalculations { get; set; }
        public virtual ICollection<RelatedCompetencyWithSensibleEvent> RelatedCompetencyWithSensibleEvents { get; set; }
        public virtual ICollection<CoacherTruth> CoacherTruths { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public bool IsPriorPeriodTransition { get; set; }
        public int? PriorPeriodDefinitionId { get; set; }
        public int? PriorEvaluationBehaviouralCompetencyId { get; set; }
    }
}
