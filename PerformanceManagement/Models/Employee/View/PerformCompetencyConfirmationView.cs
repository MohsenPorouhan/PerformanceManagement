using System.ComponentModel.DataAnnotations.Schema;


namespace PerformanceManagement.Models.Employee.View
{
    [NotMapped]
    public class PerformCompetencyConfirmationView
    {
        public int?[] EvaluationCompetencyId { get; set; }//use in coacher
        public int? EvaluationCompetencyId2 { get; set; }//use in employee
        public int? EvaluationCompetencyAcceptanceStatusId { get; set; }
        public string RefutationCause { get; set; }
        public int? AllocatorDepartmentId { get; set; }
        public int? PeriodDefinitionId { get; set; }
    }
}
