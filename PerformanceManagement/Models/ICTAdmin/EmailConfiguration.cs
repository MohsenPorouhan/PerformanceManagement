using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.ICTAdmin
{
    [NotMapped]
    public class EmailConfiguration
    {
        //public int PersonId { get; set; }
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string  UserName { get; set; }
        public string Password { get; set; }
    } 
}
