using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.View
{
    [NotMapped]
    public class SubTaskView
    {
        public int SubTaskId { get; set; }
        public string TrainingNeed { get; set; }
        public int? ParticipantId { get; set; }
        public int? ParticipantDepartmentId { get; set; }
    }
}
