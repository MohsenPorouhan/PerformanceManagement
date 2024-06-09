using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    [NotMapped]
    public class ParticipantView
    {
        public int? ParentEvaluationHierarchyId { get; set; }
        public int EvaluationHierarchyId { get; set; }
        public string ShortName { get; set; }
        public int PeopleId { get; set; }
        public string fullname { get; set; }
        public int SupervisorId { get; set; }
    }
}
