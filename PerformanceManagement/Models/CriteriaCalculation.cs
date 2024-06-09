using System;

namespace PerformanceManagement.Models
{
    public class CriteriaCalculation
    {
        public int CriteriaCalculationId { get; set; }
        public int EvaluationId { get; set; }
        public Evaluation Evaluation { get; set; }
        public int CriteriaWeightId { get; set; }
        public CriteriaWeight CriteriaWeight { get; set; }
        public double CalculatedCriteriaScore { get; set; }
        public DateTime CreatedDate { get; set; }
        public string RoleId { get; set; }
    }
}
