using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.Models;
using PerformanceManagement.Models.HRAdmin;

namespace PerformanceManagement.Util
{
    public class ScoreViewRequirement : IAuthorizationRequirement
    {
        public ScoreViewRequirement()
        {
            
        }
    }
}
