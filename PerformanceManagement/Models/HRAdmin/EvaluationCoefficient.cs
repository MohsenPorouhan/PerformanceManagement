using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class EvaluationCoefficient
    {
        public int EvaluationCoefficientId { get; set; }
        public int level1CoacherTWith { get; set; }
        public int level2CoacherTWith { get; set; }
        public int selfEvaluationTWith { get; set; }
        public int participantCoefficientT { get; set; }
        public int level1CoacherTWithout { get; set; }
        public int level2CoacherTWithout { get; set; }
        public int selfEvaluationTWithout { get; set; }
        public int level1CoacherBWith { get; set; }
        public int level2CoacherBWith { get; set; }
        public int selfEvaluationBWith { get; set; }
        public int participantCoefficientB { get; set; }
        public int level1CoacherBWithout { get; set; }
        public int level2CoacherBWithout { get; set; }
        public int selfEvaluationBWithout { get; set; }
        public virtual PeriodDefinitoion PeriodDefinitoion { get; set; }

    }
}
