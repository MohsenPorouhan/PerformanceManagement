using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    [NotMapped]
    public class Chart
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Parent { get; set; }
        public int? PositionType { get; set; }
    }
    [NotMapped]
    public class Chart2
    {
        public int EvaluationHierarchyId { get; set; }
        public string ShortName { get; set; }
        public int ParentEvaluationHierarchyId { get; set; }
        public int? PositionType { get; set; }
    }
}
