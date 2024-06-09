using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.View
{
    [NotMapped]
    public class HierarchyWithSeparatorDepartmentView
    {
        public string Levell { get; set; }
        public int PeopleId { get; set; }
        public int EvalHierarchyId { get; set; }
        public string pathh { get; set; }
        public string Department { get; set; }
        public string Chairmanship { get; set; }
        public string Management { get; set; }
        public string VicePresident { get; set; }
        public int Intermediary { get; set; }
    }
}
