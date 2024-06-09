using PerformanceManagement.Models.HRAdmin.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.Coacher.View
{
    [NotMapped]
    public class TaskView
    {
        public int TaskId { get; set; }
        public string TaskTitle { get; set; }
        public ICollection<CriteriaView> CriteriaViews { get; set; }
    }
}
