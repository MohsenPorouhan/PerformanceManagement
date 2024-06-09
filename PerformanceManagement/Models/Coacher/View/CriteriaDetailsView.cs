using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.View
{
    [NotMapped]
    public class CriteriaDetailsView
    {
        public int TaskId { get; set; }
        public string Description { get; set; }
        public string RoleId { get; set; }
        public int? CriteriaId { get; set; }
        public string Title { get; set; }
        public int? CriteriaWeightId { get; set; }
        public int? Weight { get; set; }
        public string limitOfAdmission { get; set; }
        public string CalculationWay { get; set; }
        public string CriteriaType { get; set; }
        public int? PeriodDefinitionId { get; set; }
        public string PeriodCode { get; set; }
        public string PeriodTitle { get; set; }
    }
}
