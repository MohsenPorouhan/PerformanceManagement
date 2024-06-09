using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class PersonPeriodEvaluationHierarchy
    {
        public int PersonPeriodEvaluationHierarchyId { get; set; }
        public int PeopleId { get; set; }
        public DateTime? PeopleEffectiveStartDate { get; set; }
        public People people { get; set; }
        public int PeriodDefinitoionId { get; set; }
        public PeriodDefinitoion periodDefinitoion { get; set; }
        public int EvaluationHierarchyId { get; set; }
        public DateTime? EvaluationHierarchyEffectiveStartDate { get; set; }
        public EvaluationHierarchy evaluationHierarchy { get; set; }
    }
}
