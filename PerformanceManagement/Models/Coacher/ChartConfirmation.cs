using PerformanceManagement.Models.HRAdmin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.Coacher
{
    public class ChartConfirmation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChartConfirmationId { get; set; }
        public int EvaluationHierarchyId { get; set; }
        public DateTime? EvalHieEffectiveStartDate { get; set; }
        public EvaluationHierarchy EvaluationHierarchy { get; set; }
        public int PeriodDefinitionId { get; set; }
        public PeriodDefinitoion PeriodDefinitoion { get; set; }
        public bool Confirmation { get; set; }
        public string CauseDescription { get; set; }
        public int SupervisorId { get; set; }
        public DateTime? SupervisorEffectiveStartDate { get; set; }
        public People Supervisor { get; set; }
        public int CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
