using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.View
{
    [NotMapped]
    public class GetRelatedPeopleWithExtendSectionPeriodView
    {
        public int ExtendSectionPeriodWithPeopleId { get; set; }
        public string EmployeeFullName { get; set; }
        public string EmployeeDepartmentName { get; set; }
    }
}
