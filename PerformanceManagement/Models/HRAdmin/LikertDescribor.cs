using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class LikertDescribor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LikertDescriborId { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public DateTime? EffectiveEndDate { get; set; }
        public int NumberScale { get; set; }
        public string TitleScale { get; set; }
        public int LikertScaleId { get; set; }
        public DateTime? LikertScaleEffectiveStartDate { get; set; }
        public LikertScale LikertScale { get; set; }
    }
}
