using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.Coacher.View
{
    [NotMapped]
    public class DeleteCompetencyView
    {
        public int PeriodDefinitionId { get; set; }
        public int? AllocatorDepartmentId { get; set; }
        public int AllocatorPersonId { get; set; }
        public string CompetencyTitle { get; set; }
        public int BehaviouralCompetencyId { get; set; }
        public int? HasParticipant { get; set; }
        public int EvaluationBehaviouralCompetencyId { get; set; }

    }
}
