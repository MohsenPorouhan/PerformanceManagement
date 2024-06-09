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
    public class CorrespondentJob
    {
        public int CorrespondentJobId { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
        public int BehaviouralCompetencyId { get; set; }
        public BehaviouralCompetency BehaviouralCompetency { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
