using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.View
{
    [NotMapped]
    public class EmployeeView
    {
        public int AllocatorDepartmentId { get; set; }
        public string[] Receiver { get; set; }
    }
}
