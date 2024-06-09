using PerformanceManagement.Models.HRAdmin.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.Coacher.View
{
    [NotMapped]
    public class SubTaskDivisionView
    {
        public int TaskId { get; set; }
        public int HierarchyId { get; set; }
        public string SubTaskTitle { get; set; }
        public List<CriteriaView> CriteriaViews { get; set; }
    }
}
