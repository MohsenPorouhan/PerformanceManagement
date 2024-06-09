using PerformanceManagement.Models.HRAdmin.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.PlanningAdmin.View
{
    [NotMapped]
    public class AddCriteriaView
    {
        public int PeriodDefinitoionId { get; set; }
        public int TaskId { get; set; }
        public int CriteriaType { get; set; }
        public string Criteria { get; set; }
        public string CalculationWay { get; set; }
        public string LimitOfAdmission { get; set; }
    }
}
