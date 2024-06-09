using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.Coacher.View
{
    [NotMapped]
    public class ProtestResponseView
    {
        public int ProtestResponseId { get; set; }
        public int ProtestId { get; set; }
        public string Response { get; set; }
        public int CoacherLevel { get; set; }
        public int PersonId { get; set; }
        public string CoacherFullName { get; set; }
        public int PersonDepartmentId { get; set; }
        public string CoacherDepartmentName { get; set; }
        public string RoleType { get; set; }
        public string RoleName { get; set; }
    }
}
