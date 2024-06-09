using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.Coacher.View
{
    [NotMapped]
    public class RelatedTaskCompetencyView
    {
        public int SensibleEventId { get; set; }
        public string TBEvalId { get; set; }
        public int TBId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
    }
}
