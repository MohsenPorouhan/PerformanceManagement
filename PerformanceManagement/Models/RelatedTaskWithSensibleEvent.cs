using PerformanceManagement.Models.Coacher;
using PerformanceManagement.Models.HRAdmin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class RelatedTaskWithSensibleEvent
    {
        public int RelatedTaskWithSensibleEventId { get; set; }
        public int SensibleEventId { get; set; }
        public SensibleEvent SensibleEvent { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
        public int EvaluationId { get; set; }
        public Evaluation Evaluation { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
