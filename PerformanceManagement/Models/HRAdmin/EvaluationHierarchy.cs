using PerformanceManagement.Models.Coacher;
using PerformanceManagement.Models.HRAdmin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class EvaluationHierarchy
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EvaluationHierarchyId { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public int? ParentEvaluationHierarchyId { get; set; }
        public DateTime? ParentEffectiveStartDate { get; set; }
        public virtual EvaluationHierarchy Parent { get; set; }
        public virtual ICollection<EvaluationHierarchy> Children { get; set; }
        // [ForeignKey("Supervisor")]
        public int SupervisorId { get; set; }
        public DateTime? SupervisorEffectiveStartDate { get; set; }
        public virtual People Supervisor { get; set; }
        public DateTime? EffectiveEndDate { get; set; }
        //  [ForeignKey("CreatedBy")]
        public int CreatedById { get; set; }
        public DateTime? CreatedByEffectiveStartDate { get; set; }
        public People CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        //[ForeignKey("LastUpdatedBy")]
        public int? LastUpdatedById { get; set; }
        public DateTime? LastUpdatedByEffectiveStartDate { get; set; }
        public People LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public int DepartmentType { get; set; }
        public int DepartmentId { get; set; }
        public DateTime? DepartmentEffectiveStartDate { get; set; }
        public Department Department { get; set; }
        public virtual ICollection<People> peopledepartmentHierarchy { get; set; }
        public virtual ICollection<PersonPeriodEvaluationHierarchy> PersonAndPeriodAndEvaluationHierarchies { get; set; }
        public virtual ICollection<ChartConfirmation> ChartConfirmations { get; set; }
        public virtual ICollection<Evaluation> AllocatorEvaluationHierarchies { get; set; }
        public virtual ICollection<Evaluation> RecieverAllocationEvaluationHierarchies { get; set; }
        public virtual ICollection<EvaluationBehaviouralCompetency> AllocatorEvaluationBehaviouralHierarchies { get; set; }
        public virtual ICollection<EvaluationBehaviouralCompetency> RecieverAllocationEvaluationBehaviouralHierarchies { get; set; }
        public virtual ICollection<EvaluationBehaviouralParticipant> EvaluationBehaviouralParticipants { get; set; }
        public virtual ICollection<EvaluationParticipant> EvaluationParticipants { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<FinalScoreCalculation> AllocatorFinalScoreCalculations { get; set; }
        public virtual ICollection<FinalScoreCalculation> RecieverFinalScoreCalculations { get; set; }
        public virtual ICollection<FinalScoreCompetencyCalculation> AllocatorFinalScoreCompetencyCalculations { get; set; }
        public virtual ICollection<FinalScoreCompetencyCalculation> RecieverFinalScoreCompetencyCalculations { get; set; }
        public virtual ICollection<SensibleEvent> SensibleEvents { get; set; }
        public virtual ICollection<SensibleEvent> EmployeeSensibleEvent { get; set; }
        public virtual ICollection<ExtendSectionPeriodWithPeople> ExtendSectionPeriodWithPeople { get; set; }
        public virtual ICollection<Protest> Protests { get; set; }
        public virtual ICollection<Addressee> Addressees { get; set; }
        public virtual ICollection<ProtestResponse> ProtestResponses { get; set; }
        public virtual ICollection<ChartInfo> ChartInfos { get; set; }
    }
}
