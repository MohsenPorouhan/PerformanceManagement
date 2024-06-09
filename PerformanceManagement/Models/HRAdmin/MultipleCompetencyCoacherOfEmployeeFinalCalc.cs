using System;

namespace PerformanceManagement.Models.HRAdmin
{
    public class MultipleCompetencyCoacherOfEmployeeFinalCalc
    {
        public int MultipleCompetencyCoacherOfEmployeeFinalCalcId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? EmployeeEffectiveStartDate { get; set; }
        public People People { get; set; }
        public int PeriodDefinitionId { get; set; }
        public PeriodDefinitoion PeriodDefinition { get; set; }
        public double? FinalCompetencyScore { get; set; }
        public int? ApplyCompetencyPercent { get; set; }
    }
}
