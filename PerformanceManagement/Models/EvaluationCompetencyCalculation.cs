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
    public class EvaluationCompetencyCalculation
    {
        public int EvaluationCompetencyCalculationId { get; set; }
        public int EvaluationBehaviouralCompetencyId { get; set; }
        public EvaluationBehaviouralCompetency EvaluationBehaviouralCompetency { get; set; }
        public double? CalculatedCompetencyScore { get; set; }
        public int CoacherId { get; set; }
        public int CoacherDepartmentId { get; set; }
        public int EmployeeId { get; set; }
        public int EmployeeDepartmentId { get; set; }
        public int PeriodDefinitionId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
