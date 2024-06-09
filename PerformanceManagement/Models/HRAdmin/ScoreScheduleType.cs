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
    public class ScoreScheduleType
    {
        public int ScoreScheduleTypeId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<ScoreSchedule> ScoreSchedules { get; set; }
        public virtual ICollection<ExtendScoreSchedule> ExtendScoreSchedules { get; set; }

    }
}
