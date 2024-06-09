using PerformanceManagement.Models.HRAdmin.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.Coacher.View
{
    [NotMapped]
    public class TruthView
    {
        public string TruthTitle { get; set; }
        public string Creator { get; set; }
    }
}
