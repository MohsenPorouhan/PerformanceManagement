using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.View
{
    [NotMapped]
    public class PeriodDefinitionVieweeee
    {
        public int PeriodDefinitoionId { get; set; }
        public int PeriodCode { get; set; }
        public string PeriodTitle { get; set; }
        public DateTime periodDefinitionDateFrom { get; set; }
        public DateTime periodDefinitionDateTo { get; set; }
        public int SectionPeriodId { get; set; }
        public int StatusCode { get; set; }
        public DateTime periodDefinitionInitialDateFrom { get; set; }
        public DateTime periodDefinitionInitialDateTo { get; set; }
        public DateTime periodDefinitionFinalDateFrom { get; set; }
        public DateTime periodDefinitionFinalDateTo { get; set; }
        public DateTime periodDefinitionProtestDateFrom { get; set; }
        public DateTime periodDefinitionProtestDateTo { get; set; }
    }
}
