using PerformanceManagement.Models.HRAdmin.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.Coacher.View
{
    [NotMapped]
    public class PriorTaskOfPeriodView
    {
        public int id { get; set; }
        public string label { get; set; }
        public string value { get; set; }
    }
}
