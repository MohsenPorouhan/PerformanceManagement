using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin
{
    public class ExtendSectionPeriodWithPeople
    {
        public int ExtendSectionPeriodWithPeopleId { get; set; }
        public int ExtendSectionPeriodId { get; set; }
        public ExtendSectionPeriod ExtendSectionPeriod { get; set; }
        public int PeopleId { get; set; }
        public DateTime? PeopleEffectiveStartDate { get; set; }
        public People People { get; set; }
        public int EvaluationHierarchyId { get; set; }
        public DateTime? EvaluationHierarchyEffectiveStartDate { get; set; }
        public EvaluationHierarchy EvaluationHierarchy { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
