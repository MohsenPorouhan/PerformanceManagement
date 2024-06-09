using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    [NotMapped]
    public class ExtendSectionPeriodView
    {
        public int ExtendSectionPeriodId { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string PeriodTitle { get; set; }
        public string SectionName { get; set; }
        public int? StatusCode { get; set; }
        public ICollection<ExtendScoreScheduleView> ExtendScoreScheduleViews { get; set; }
    }
}
