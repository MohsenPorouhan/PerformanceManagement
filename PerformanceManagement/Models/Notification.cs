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
    public class Notification
    {
        public int NotificationId { get; set; }
        public string Title { get; set; }
        public int? NotificationTitleId { get; set; }
        public NotificationTitle NotificationTitle { get; set; }
        public string Description { get; set; }
        public int ReceiverPersonId { get; set; }
        public int? ReceiverHierarchtId { get; set; }
        public int? EvaluationId { get; set; }
        public int AllocatorPersonId { get; set; }
        public int? AllocatorHierarchyId { get; set; }
        public string AllocatorRoleId { get; set; }
        public string ReceiverRoleId { get; set; }
        public int? PeriodDefinitionId { get; set; }
        public bool Visibility { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
