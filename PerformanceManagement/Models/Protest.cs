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
    public class Protest
    {
        public int ProtestId { get; set; }
        public string Description { get; set; }
        public bool? Confirmation { get; set; }
        public bool VisibilityToHierarchy { get; set; }
        public int ProtesterId { get; set; }
        public DateTime? ProtesterEffectiveStartDate { get; set; }
        public People Protester { get; set; }
        public int ProtesterDepartmentId { get; set; }
        public DateTime? ProtesterDepartmnetEffectiveStartDate { get; set; }
        public EvaluationHierarchy ProtesterDepartment { get; set; }
        public int PeriodDefinitionId { get; set; }
        public PeriodDefinitoion PeriodDefinition { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public virtual ICollection<Addressee> Addressees { get; set; }
        public virtual ICollection<ProtestResponse> ProtestResponses { get; set; }

    }
}
