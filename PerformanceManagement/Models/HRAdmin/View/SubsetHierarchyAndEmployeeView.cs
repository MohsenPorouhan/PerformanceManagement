using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.View
{
    [NotMapped]
    public class SubsetHierarchyAndEmployeeView
    {
        public int EvaluationHierarchyId { get; set; }
        public string text { get; set; }
        public string parent { get; set; }
        public int? Levell { get; set; }
        public int PeopleId { get; set; }
        public int EvalHierarchyId { get; set; }

    }
}
