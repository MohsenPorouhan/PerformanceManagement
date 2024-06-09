using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.View
{
    [NotMapped]
    public class RelatedScoreScheduleView
    {
        public int ScoreScheduleId { get; set; }
        public int PeriodDefinitionId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int ScoreScheduleTypeId { get; set; }
        public string Title { get; set; }
    }
}
