using PerformanceManagement.Models;
using PerformanceManagement.Models.HRAdmin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.ViewModels
{
    public class AccessControlDecisionViewModel
    {
        public int? PeriodDefinitionId { get; set; }
        public int? PeopleId { get; set; }
        public int? DepartmentId { get; set; }
        public AppDbContext AppDbContext { get; set; }
        public int? ScoreScheduleTypeId { get; set; }
    }
}
