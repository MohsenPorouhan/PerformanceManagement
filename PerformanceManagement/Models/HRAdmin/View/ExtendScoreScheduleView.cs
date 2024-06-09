using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    [NotMapped]
    public class ExtendScoreScheduleView
    {
        public int ExtendScoreScheduleId { get; set; }
        public int ScoreScheduleTypeId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
