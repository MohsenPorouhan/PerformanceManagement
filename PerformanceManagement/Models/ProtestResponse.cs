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
    public class ProtestResponse
    {
        public int ProtestResponseId { get; set; }
        public int ProtestId { get; set; }
        public Protest Protest { get; set; }
        public string Response { get; set; }
        public int? CoacherLevel { get; set; }
        public int PersonId { get; set; }
        public DateTime? PersonEffectiveStartDate { get; set; }
        public People Person { get; set; }
        public int? PersonDepartmentId { get; set; }
        public DateTime? PersonDepartmnetEffectiveStartDate { get; set; }
        public EvaluationHierarchy PersonDepartment { get; set; }
        public string RoleType { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
