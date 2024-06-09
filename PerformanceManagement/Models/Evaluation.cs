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
    public class Evaluation
    {
        public int EvaluationId { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
        public int? TaskWeight { get; set; }
        public DateTime? AllocatorEvalHieEffcStartDate { get; set; }
        public int? AllocatorEvaluationHierarchyId { get; set; }
        public EvaluationHierarchy AllocatorEvaluationHierarchy { get; set; }
        public DateTime? RecieverAllocEvalHieEffcStartDate { get; set; }
        public int RecieverAllocationEvaluationHierarchyId { get; set; }
        public EvaluationHierarchy RecieverAllocationEvaluationHierarchy { get; set; }
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
        public virtual ICollection<CriteriaWeight> CriteriaWeights { get; set; }
        public virtual ICollection<EvaluationScore> EvaluationScores { get; set; }
        public virtual ICollection<EvaluationParticipant> EvaluationParticipants { get; set; }
        public virtual ICollection<TrainingNeed> TrainingNeeds { get; set; }
        public virtual ICollection<EvaluationCalculation> EvaluationCalculations { get; set; }
        public virtual ICollection<CriteriaCalculation> CriteriaCalculations { get; set; }
        public virtual ICollection<RelatedTaskWithSensibleEvent> RelatedTaskWithSensibleEvents { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public bool IsPriorPeriodTransition { get; set; }
        public int? PriorPeriodDefinitionId { get; set; }
        public int? PriorEvaluationId { get; set; }

    }
}
