using PerformanceManagement.Models.HRAdmin.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.PlanningAdmin.View
{
    [NotMapped]
    public class CriteriaWeightView2
    {
        public int CriteriaId { get; set; }
        public int? CriteriaWeightId { get; set; }
        public int Weight { get; set; }
        public int? EvaluationId { get; set; }
    }
}
