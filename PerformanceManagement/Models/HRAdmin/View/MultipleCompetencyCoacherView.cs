using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.View
{
    [NotMapped]
    public class MultipleCompetencyCoacherView
    {
        public int FinalScoreCompetencyCalculationId { get; set; }
        public int EmployeeId { get; set; }
        public int PeriodDefinitoionId { get; set; }
        public double CompetencyWeight { get; set; }
    }
}
