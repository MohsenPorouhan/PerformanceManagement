using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.ICTAdmin
{
    public class ApplicationUser: IdentityUser
    {
        //public int PersonId { get; set; }
        public People People { get; set; }
        public string CreatedBy { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public DateTime EffectiveEndDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string LastLoginIpAddress { get; set; }
        public DateTime? LastChangedPasswordDate { get; set; }
        public DateTime? LastMustChangedPasswordDate { get; set; }
        public bool? MustChangePassword { get; set; }
        public DateTime? LastResetPasswordDate { get; set; }
        public int? LastResetPasswordBy { get; set; }
        public long? IdNumber { get; set; }
    } 
}
