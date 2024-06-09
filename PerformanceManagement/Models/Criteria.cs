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
    public class Criteria
    {
        public int CriteriaId { get; set; }
        public string Title { get; set; }
        public string LimitOfAdmission { get; set; }
        public int? CriteriaType { get; set; }
        public string CalculationWay { get; set; }
        public int? PeriodDefinitionId { get; set; }
        public bool IsProcessingCriteria { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
        public virtual ICollection<CriteriaWeight> CriteriaWeights { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public int? PriorCriteriaId { get; set; }
    }
}
