using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.Employee.View
{
    [NotMapped]
    //use in employee and coacher
    public class PerformConfirmationView
    {
        public int?[] EvaluationId { get; set; }
        public int? EvaluationId2 { get; set; }
        public int? EvaluationAcceptanceStatusId { get; set; }
        public string RefutationCause { get; set; }
        public int? DepartmnetId { get; set; }
        public int? PeriodDefinitionId { get; set; }
    }
}
