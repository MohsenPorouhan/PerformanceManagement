using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.View
{
    [NotMapped]
    public class CoveredEmployee
    {
        public int PeriodDefinitionId { get; set; }
        public int AllocatorDepartmentId { get; set; }
        public string[] Receiver { get; set; }
        public ICollection<SubTaskView> subTaskViews { get; set; }
        public ICollection<TextTaskView> textTaskViews { get; set; }
    }
}
