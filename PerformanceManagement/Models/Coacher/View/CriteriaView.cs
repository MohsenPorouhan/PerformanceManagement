using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.View
{
    [NotMapped]
    public class CriteriaView
    {
        public int? CriteriaId { get; set; }
        public string Title { get; set; }
        public string limitOfAdmission { get; set; }
        public string CalculationWay { get; set; }
        public bool IsProcessingCriteria { get; set; }
    }
}
