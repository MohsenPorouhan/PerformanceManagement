using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.Coacher.View
{
    [NotMapped]
    public class ScoreView
    {
        public int EvaluationId { get; set; }
        public string Title { get; set; }
        public int CriteriaWeightId { get; set; }
        public int CriteriaId { get; set; }
        public string CriteriaTitle { get; set; }
        public string LimitOfAdmission { get; set; }
        public string CalculationWay { get; set; }
        public int? score1 { get; set; }
        public int? score2 { get; set; }
        public int? score3 { get; set; }
        public int? score4 { get; set; }
        public int? participantScore { get; set; }
        public int? selfScore { get; set; }
        public int? planningAdminScore { get; set; }
    }
}
