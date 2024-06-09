using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.View
{
    [NotMapped]
    public class EvaluationCompetencyView
    {
        public int PeriodDefinitionId { get; set; }
        public int BehaviouralCompetencyId { get; set; }
        public int EvaluationBehaviouralCompetencyId { get; set; }
        public int? CompetencyWeight { get; set; }
        public int? CompetencyScore { get; set; }
        public int? CoacherLevel { get; set; }
        public int? AllocatorDepartmentId { get; set; }
        public int? RecieverAllocationEvaluationBehaviouralHierarchyId { get; set; }
        public int? RecieverAllocationPersonId { get; set; }
    }
}
