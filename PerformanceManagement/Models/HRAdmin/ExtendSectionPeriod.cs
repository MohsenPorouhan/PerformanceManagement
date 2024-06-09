using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class ExtendSectionPeriod
    {
        public int ExtendSectionPeriodId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int SectionPeriodId { get; set; }
        public SectionPeriod SectionPeriod { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public virtual ICollection<ExtendSectionPeriodWithPeople> ExtendSectionPeriodWithPeople { get; set; }
        public virtual ICollection<ExtendScoreSchedule> ExtendScoreSchedules { get; set; }

    }
}
