using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.Models;
using PerformanceManagement.Models.HRAdmin;
using Task = System.Threading.Tasks.Task;

namespace PerformanceManagement.Util
{
    public class MustChangePasswordRequirement : IAuthorizationRequirement
    {
        //AppDbContext _context;
        //IHttpContextAccessor _contextAccessor;

        public MustChangePasswordRequirement()
        {
            
        }
        //public async Task<bool> Pass(AppDbContext context, IHttpContextAccessor contextAccessor,string area,string controller,string action,string id)
        //{
        //    _context = context;
        //    _contextAccessor = contextAccessor;
        //    //
        //    return await Task.FromResult(false);
        //}
    }
}
