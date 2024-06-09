using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerformanceManagement.Models
{
    public class CriteriaWeight
    {
        public int CriteriaWeightId { get; set; }
        [Required]
        public int Weight { get; set; }
        public int EvaluationId { get; set; }
        public Evaluation Evaluation { get; set; }
        public int CriteriaId { get; set; }
        public Criteria Criteria { get; set; }
        [Required]
        public string RoleId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public virtual ICollection<CriteriaWeightScore> CriteriaWeightScores { get; set; }
        public virtual ICollection<CriteriaCalculation> CriteriaCalculations { get; set; }
    }
}
