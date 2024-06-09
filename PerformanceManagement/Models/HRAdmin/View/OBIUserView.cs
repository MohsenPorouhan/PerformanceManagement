using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.View
{
    [NotMapped]
    public class OBIUserView
    {
        public string Id { get; set; }
        public string U_NAME { get; set; }
        public string MAIL_ADDRESS { get; set; }
        public string U_PASSWORD { get; set; }
        public string U_DESCRIPTION { get; set; }
    }
}
