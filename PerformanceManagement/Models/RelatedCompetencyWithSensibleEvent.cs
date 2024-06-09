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
    public class RelatedCompetencyWithSensibleEvent
    {
        public int RelatedCompetencyWithSensibleEventId { get; set; }
        public int SensibleEventId { get; set; }
        public SensibleEvent SensibleEvent { get; set; }
        public int BehaviouralCompetencyId { get; set; }
        public BehaviouralCompetency BehaviouralCompetency { get; set; }
        public int EvaluationBehaviouralCompetencyId { get; set; }
        public EvaluationBehaviouralCompetency EvaluationBehaviouralCompetency { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
