using PerformanceManagement.Models.Coacher;
using PerformanceManagement.Models.HRAdmin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.Coacher
{
    public class CoacherTruth
    {
        public int CoacherTruthId { get; set; }
        public string Title { get; set; }
        public int EvaluationBehaviouralCompetencyId { get; set; }
        public EvaluationBehaviouralCompetency EvaluationBehaviouralCompetency { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
