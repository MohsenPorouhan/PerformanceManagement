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
    public class People
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PeopleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public int? changeStatus { get; set; }
        public DateTime? EffectiveEndDate { get; set; }
        public int? PositionType { get; set; }
        public int? EvaluationHierarchyID { get; set; }
        public DateTime? EvaluationHierarchyDate { get; set; }
        public EvaluationHierarchy evaluationHierarchy { get; set; }
        public int? JobId { get; set; }
        public Job Job { get; set; }
        public virtual ICollection<EvaluationHierarchy> EvaluationHierarchies { get; set; }
        public virtual ICollection<EvaluationHierarchy> Createdby { get; set; }
        public virtual ICollection<EvaluationHierarchy> LastUpdatedBy { get; set; }
        public virtual ICollection<PersonPeriodEvaluationHierarchy> PersonAndPeriods { get; set; }
        public virtual ICollection<ChartConfirmation> ChartConfirmations { get; set; }
        public virtual ICollection<Evaluation> Allocators { get; set; }
        public virtual ICollection<Evaluation> RecieversAllocation { get; set; }
        public virtual ICollection<EvaluationScore> EvaluationScores { get; set; }
        public virtual ICollection<CriteriaWeightScore> CriteriaWeightScores { get; set; }
        public virtual ICollection<EvaluationBehaviouralCompetency> AllocatorsEvaluationBehavioural { get; set; }
        public virtual ICollection<EvaluationBehaviouralCompetency> RecieversAllocationEvaluationBehavioural { get; set; }
        public virtual ICollection<EvaluationBehaviourCompetencyScore> EvaluationBehaviourCompetencyScores { get; set; }
        public virtual ICollection<EvaluationBehaviouralParticipant> EvaluationBehaviouralParticipants { get; set; }
        public virtual ICollection<EvaluationParticipant> EvaluationParticipants { get; set; }
        public virtual ICollection<FinalScoreCalculation> AllocatorFinalScoreCalculations { get; set; }
        public virtual ICollection<FinalScoreCalculation> RecieverFinalScoreCalculations { get; set; }
        public virtual ICollection<FinalScoreCompetencyCalculation> AllocatorFinalScoreCompetencyCalculations { get; set; }
        public virtual ICollection<FinalScoreCompetencyCalculation> RecieverFinalScoreCompetencyCalculations { get; set; }
        public virtual ICollection<FinalizeCalculation> FinalizeCalculations { get; set; }
        public virtual ICollection<SensibleEvent> SensibleEvents { get; set; }
        public virtual ICollection<SensibleEvent> EmployeeSensibleEvents { get; set; }
        public virtual ICollection<ExtendSectionPeriodWithPeople> ExtendSectionPeriodWithPeople { get; set; }
        public virtual ICollection<MultipleTaskCoacherOfEmployeeFinalCalc> MultipleTaskCoacherOfEmployeeFinalCalcs { get; set; }
        public virtual ICollection<MultipleCompetencyCoacherOfEmployeeFinalCalc> MultipleCompetencyCoacherOfEmployeeFinalCalcs { get; set; }
        public virtual ICollection<Protest> Protests { get; set; }
        public virtual ICollection<Addressee> Addressees { get; set; }
        public virtual ICollection<ProtestResponse> ProtestResponses { get; set; }
        public virtual ICollection<ChartInfo> ChartInfos { get; set; }


    }
}
