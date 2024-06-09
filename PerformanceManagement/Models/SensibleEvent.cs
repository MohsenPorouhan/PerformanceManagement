using Microsoft.AspNetCore.Http;
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
    public class SensibleEvent
    {
        public int SensibleEventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int EventType { get; set; }
        public DateTime SensibleEventDate { get; set; }
        public bool Visibility { get; set; }
        public int PersonId { get; set; }
        public DateTime? PersonEffectiveStartDate { get; set; }
        public People Person { get; set; }
        public int PersonDepartmentId { get; set; }
        public DateTime? DepartmentEffectiveStartDate { get; set; }
        public EvaluationHierarchy PersonDepartment { get; set; }
        public string PersonRole { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? EmployeeEffectiveStartDate { get; set; }
        public People Employee { get; set; }
        public int? EmployeeDepartmentId { get; set; }
        public DateTime? EmployeeDepartmentEffectiveStartDate { get; set; }
        public EvaluationHierarchy EmployeeDepartment { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public int PeriodDefinitionId { get; set; }
        public PeriodDefinitoion PeriodDefinition { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<RelatedCompetencyWithSensibleEvent> RelatedCompetencyWithSensibleEvents { get; set; }
        public virtual ICollection<RelatedTaskWithSensibleEvent> RelatedTaskWithSensibleEvents { get; set; }
    }
}
