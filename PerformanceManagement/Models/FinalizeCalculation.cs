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
    public class FinalizeCalculation
    {
        public int FinalizeCalculationId { get; set; }
        public DateTime? CoacherEffecStartDate { get; set; }
        public int? CocherId { get; set; }
        public People Coacher { get; set; }
        public int PeriodDefinitoionId { get; set; }
        public PeriodDefinitoion PeriodDefinitoion { get; set; }
        public bool IsFinalization { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string RoleId { get; set; }
    }
}
