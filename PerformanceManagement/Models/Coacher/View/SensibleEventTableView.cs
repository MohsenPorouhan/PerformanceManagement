using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.Coacher.View
{
    [NotMapped]
    public class SensibleEventTableView
    {
        public int SensibleEventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TaskTitle { get; set; }
        public int EventType { get; set; }
        public int PersonId { get; set; }
        public string CreatedPersonFullName { get; set; }
        public int PersonDepartmentId { get; set; }
        public string CreatedDepartmentName { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeFullName { get; set; }
        public int? EmployeeDepartmentId { get; set; }
        public string EmployeeDepartmentName { get; set; }
        public string PersonRole { get; set; }
        public string RoleName { get; set; }
        public int Visibility { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime SensibleEventDate { get; set; }
        public int PeriodDefinitionId { get; set; }
    }
}
