using System;

namespace PerformanceManagement.Models.HRAdmin
{
    public class MultipleTaskCoacherOfEmployeeFinalCalc
    {
        public int MultipleTaskCoacherOfEmployeeFinalCalcId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? EmployeeEffectiveStartDate { get; set; }
        public People People { get; set; }
        public int PeriodDefinitionId { get; set; }
        public PeriodDefinitoion PeriodDefinition { get; set; }
        public double? FinalTaskScore { get; set; }
        public int? ApplyTaskPercent { get; set; }
    }
}
