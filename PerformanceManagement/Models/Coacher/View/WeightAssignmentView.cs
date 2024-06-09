using PerformanceManagement.Models.HRAdmin.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.Coacher.View
{
    [NotMapped]
    public class WeightAssignmentView
    {
        public int TaskId { get; set; }
        public int TaskWeight { get; set; }
        public int EvaluationId { get; set; }
        public int PeriodDefinitoionId { get; set; }
        public int AllocatorEvaluationHierarchyId { get; set; }
        public int RecieverAllocationEvaluationHierarchyId { get; set; }
        public int RecieverAllocationPersonId { get; set; }
        public ICollection<CriteriaWeightView> CriteriaWeightViews { get; set; }
    }
}
