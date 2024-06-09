using PerformanceManagement.Models.Coacher;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    [NotMapped]
    public class PeriodDefinitoionView
    {
        public int PeriodDefinitoionId { get; set; }
        public string PeriodCode { get; set; }
        public string PeriodTitle { get; set; }
        public string PeriodFrom { get; set; }
        public string PeriodTo { get; set; }
        public string InitialDateFrom { get; set; }
        public string InitialDateTo { get; set; }
        public string FinalDateFrom { get; set; }
        public string FinalDateTo { get; set; }
        public string ProtestDateFrom { get; set; }
        public string ProtestDateTo { get; set; }
    }
}
