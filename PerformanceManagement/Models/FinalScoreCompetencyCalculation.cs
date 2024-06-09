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
    public class FinalScoreCompetencyCalculation
    {
        public int FinalScoreCompetencyCalculationId { get; set; }
        public DateTime? AllocatorEvalHieEffcStartDate { get; set; }
        public int AllocatorEvaluationHierarchyId { get; set; }
        public EvaluationHierarchy AllocatorEvaluationHierarchy { get; set; }
        public DateTime? RecieverAllocEvalHieEffcStartDate { get; set; }
        public int RecieverAllocationEvaluationHierarchyId { get; set; }
        public EvaluationHierarchy RecieverAllocationEvaluationHierarchy { get; set; }
        public DateTime? AllocatorPersonEffecStartDate { get; set; }
        public int AllocatorPersonId { get; set; }
        public People Allocator { get; set; }
        public DateTime? RecieverAllocPersonEffecStartDate { get; set; }
        public int RecieverAllocationPersonId { get; set; }
        public People RecieverAllocation { get; set; }
        public int PeriodDefinitoionId { get; set; }
        public PeriodDefinitoion PeriodDefinitoion { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int AllocatorLevel { get; set; }
        public string CoacherType { get; set; }
        public double? FinalCompetneciesScore { get; set; }
        public double? ApplyPercentToFinalScore { get; set; }
        public double? MultipleCoacherWeight { get; set; }

    }
}
