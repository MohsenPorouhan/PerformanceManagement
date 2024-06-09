using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.PlanningAdmin.View
{
    [NotMapped]
    public class RelatedTaskView
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
    }
}
