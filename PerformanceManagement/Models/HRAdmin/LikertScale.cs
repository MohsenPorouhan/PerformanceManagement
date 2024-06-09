using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class LikertScale
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LikertScaleId { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public DateTime? EffectiveEndDate { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public virtual ICollection<LikertDescribor> LikertDescribors { get; set; }
        public virtual ICollection<PeriodDefinitoion> periodDefinitoionLikertBehaiviour { get; set; }
        public virtual ICollection<PeriodDefinitoion> periodDefinitoionLikertTask { get; set; }
        public virtual ICollection<PeriodDefinitoion> periodDefinitoionLikertScoreWay { get; set; }
    }
}
