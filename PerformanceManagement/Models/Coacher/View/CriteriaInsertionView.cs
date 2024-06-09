using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.Coacher.View
{
    [NotMapped]
    public class CriteriaInsertionView
    {
        public int? CriteriaId { get; set; }
        public string Title { get; set; }
        public string LimitOfAdmission { get; set; }
        public string CalculationWay { get; set; }
        public bool IsProcessingCriteria { get; set; }
    }
}
