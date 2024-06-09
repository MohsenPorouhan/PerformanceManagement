using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.View
{
    [NotMapped]
    public class RelatedExtendSectionView
    {
        public int ExtendSectionPeriodId { get; set; }
        public int SectionPeriodId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime PrimarySectionDateFrom { get; set; }
        public DateTime PrimarySectionDateTo { get; set; }
        public int StatusCode { get; set; }
        public int PeriodDefinitoionId { get; set; }
        public string PeriodTitle { get; set; }
    }
}
