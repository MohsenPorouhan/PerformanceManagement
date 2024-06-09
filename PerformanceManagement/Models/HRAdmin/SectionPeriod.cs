using System;
using System.Collections.Generic;

namespace PerformanceManagement.Models.HRAdmin
{
    public class SectionPeriod
    {
        public int SectionPeriodId { get; set; }
        public int StatusCode { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int PeriodDefinitoionId { get; set; }
        public PeriodDefinitoion periodDefinitoion { get; set; }
        public virtual ICollection<ExtendSectionPeriod> ExtendSectionPeriods { get; set; }

    }
}
