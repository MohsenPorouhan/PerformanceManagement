using System;
using System.Collections.Generic;

namespace PerformanceManagement.Models.HRAdmin
{
    public class ExtendScoreSchedule
    {
        public int ExtendScoreScheduleId { get; set; }
        public int ScoreScheduleTypeId { get; set; }
        public ScoreScheduleType ScoreScheduleType { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int ExtendSectionPeriodId { get; set; }
        public ExtendSectionPeriod ExtendSectionPeriod { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
