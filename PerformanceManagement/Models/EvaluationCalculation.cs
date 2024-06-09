using System;

namespace PerformanceManagement.Models
{
    public class EvaluationCalculation
    {
        public int EvaluationCalculationId { get; set; }
        public int EvaluationId { get; set; }
        public Evaluation Evaluation { get; set; }
        public double CalculatedScore { get; set; }
        public int? CoacherId { get; set; }
        public int? CoacherDepartmentId { get; set; }
        public int EmployeeId { get; set; }
        public int EmployeeDepartmentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int PeriodDefinitionId { get; set; }
        public string roleId { get; set; }
    }
}
