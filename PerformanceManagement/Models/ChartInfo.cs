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
    public class ChartInfo
    {
        public int ChartInfoId { get; set; }
        public int PeopleId { get; set; }
        public DateTime? PeopleEffectiveStartDate { get; set; }
        public People People { get; set; }
        public int EvaluationHierarchyId { get; set; }
        public DateTime? EvalEffectiveStartDate { get; set; }
        public EvaluationHierarchy EvaluationHierarchy { get; set; }
        public string Department { get; set; }
        public string Chairmanship { get; set; }
        public string Management { get; set; }
        public string VicePresident { get; set; }
        public int Level { get; set; }
        public bool Intermediary { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public DateTime? EffectiveEndDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastUpdatedById { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
