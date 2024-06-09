using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.View
{
    [NotMapped]
    public class MultipleTaskCoacherView
    {
        public int FinalScoreCalculationId { get; set; }
        public int EmployeeId { get; set; }
        public int PeriodDefinitoionId { get; set; }
        public double TaskWeight { get; set; }
    }
}
