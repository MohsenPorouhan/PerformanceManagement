using PerformanceManagement.Models.Coacher;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class PeriodDefinitoion
    {
        public int PeriodDefinitoionId { get; set; }
        [Required]
        public string PeriodCode { get; set; }
        [Required]
        public string PeriodTitle { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public virtual ICollection<SectionPeriod> SectionPeriods { get; set; }
        public virtual ICollection<PersonPeriodEvaluationHierarchy> PersonAndPeriods { get; set; }
        public int? LikertWeightWayBehaiviourId { get; set; }
        public DateTime? LikertWeightWayBehaiviourEffectiveStartDate { get; set; }
        public LikertScale LikertWeightWayBehaiviour { get; set; }
        public int? LikertWeightWayTaskId { get; set; }
        public DateTime? LikertWeightWayTaskEffectiveStartDate { get; set; }
        public LikertScale LikertWeightWayTask { get; set; }
        public int? LikertScoreWayId { get; set; }
        public DateTime? LikertScoreWayEffectiveStartDate { get; set; }
        public LikertScale LikertScoreWay { get; set; }
        public int? WeightWayScore { get; set; }
        public int? WeightWayTask { get; set; }
        public int? WeightWayBehaviour { get; set; }
        public float? TaskPercent { get; set; }
        public float? CompetencyPercent { get; set; }
        public int? EvaluationCoefficientId { get; set; }
        public EvaluationCoefficient EvaluationCoefficient { get; set; }
        public int? LackOfScore { get; set; }
        public virtual ICollection<ChartConfirmation> ChartConfirmations { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<Evaluation> Evaluations { get; set; }
        public virtual ICollection<EvaluationBehaviouralCompetency> EvaluationBehaviouralCompetencies { get; set; }
        public virtual ICollection<FinalScoreCalculation> FinalScoreCalculations { get; set; }
        public virtual ICollection<FinalScoreCompetencyCalculation> FinalScoreCompetencyCalculations { get; set; }
        public virtual ICollection<FinalizeCalculation> FinalizeCalculations { get; set; }
        public virtual ICollection<SensibleEvent> SensibleEvents { get; set; }
        public virtual ICollection<MultipleTaskCoacherOfEmployeeFinalCalc> MultipleTaskCoacherOfEmployeeFinalCalcs { get; set; }
        public virtual ICollection<MultipleCompetencyCoacherOfEmployeeFinalCalc> MultipleCompetencyCoacherOfEmployeeFinalCalcs { get; set; }
        public virtual ICollection<Protest> Protests { get; set; }
        public virtual ICollection<ScoreSchedule> ScoreSchedules { get; set; }
    }
}
