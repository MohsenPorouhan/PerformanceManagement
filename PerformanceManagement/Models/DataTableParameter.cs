using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    [NotMapped]
    public class DataTableParameter
    {
        public int start { get; set; }
        public int length { get; set; }
        public int draw { get; set; }
        public string search { get; set; }
        public int orderColumn { get; set; }
        public bool orderable { get; set; }
        public string orderDIR { get; set; }
    }
}
