using System;
using System.Collections.Generic;

namespace PerformanceManagement.Models.HRAdmin
{
    public class ScoreSchedule
    {
        public int ScoreScheduleId { get; set; }
        public int ScoreScheduleTypeId { get; set; }
        public ScoreScheduleType ScoreScheduleType { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int PeriodDefinitionId { get; set; }
        public PeriodDefinitoion PeriodDefinitoion { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
