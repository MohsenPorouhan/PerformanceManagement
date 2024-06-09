using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.PlanningAdmin.View
{
    [NotMapped]
    public class TaskDefinitionEditionView
    {
        public int TaskId { get; set; }
        public int PeriodDefinitionId { get; set; }
        public string Title { get; set; }
        public int? Type { get; set; }
        public int? ParentTaskId { get; set; }
        public ICollection<Criteria> Criterias { get; set; }
    }
}
