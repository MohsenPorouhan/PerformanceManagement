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
    public class EvaluationBehaviourCompetencyScore
    {
        public int EvaluationBehaviourCompetencyScoreId { get; set; }
        public int Score { get; set; }
        public int EvaluationBehaviouralCompetencyId { get; set; }
        public EvaluationBehaviouralCompetency EvaluationBehaviouralCompetency { get; set; }
        public int? CoacherId { get; set; }
        public DateTime? CoacherEffectiveStartDate { get; set; }
        public People Coacher { get; set; }
        public int? CoacherLevel { get; set; }
        public string RoleId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
