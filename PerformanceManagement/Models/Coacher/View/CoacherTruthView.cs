using PerformanceManagement.Models.HRAdmin.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.Coacher.View
{
    [NotMapped]
    public class CoacherTruthView
    {
        public int BehaviouralCompetencyId { get; set; }
        public int TruthId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public int CoacherId { get; set; }
    }
}
