using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.View
{
    [NotMapped]
    public class OBIGroupView
    {
        public string Id { get; set; }
        public string G_NAME { get; set; }
        public string DESCRIPTION { get; set; }
    }
}
