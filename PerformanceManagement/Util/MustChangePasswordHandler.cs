
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.Models;
using PerformanceManagement.Models.HRAdmin;
using PerformanceManagement.Models.HRAdmin.Services;
using PerformanceManagement.Models.ICTAdmin;
using PerformanceManagement.ViewModels;

namespace PerformanceManagement.Util
{
    public class MustChangePasswordHandler : AuthorizationHandler<MustChangePasswordRequirement>
    {
        readonly AppDbContext _context;
        readonly IHttpContextAccessor _contextAccessor;

        public MustChangePasswordHandler(AppDbContext c, IHttpContextAccessor ca)
        {
            _context = c;
            _contextAccessor = ca;
        }

        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, MustChangePasswordRequirement requirement)
        {

            //if (context.Resource is AuthorizationFilterContext filterContext)
            //{
            //    var area = (filterContext.RouteData.Values["area"] as string)?.ToLower();
            //    var controller = (filterContext.RouteData.Values["controller"] as string)?.ToLower();
            //    var action = (filterContext.RouteData.Values["action"] as string)?.ToLower();
            //    var id = (filterContext.RouteData.Values["id"] as string)?.ToLower();
            //    if (await requirement.Pass(_context,_contextAccessor,area,controller,action,id))
            //    {
            //        context.Fail();
            //    }

            //}

            var identityUserName = context.User.Identity.Name;


            MustChangePassControlViewModel mcpcvm = context.Resource as MustChangePassControlViewModel;
            string userName = null;
            if (identityUserName != null)
            {
                userName = identityUserName;
            }
            else if (mcpcvm.UserName != null)
            {
                userName = mcpcvm.UserName;
            }

            ApplicationUser applicationUsers = _context.applicationUsers.Where(c => c.UserName == userName).SingleOrDefault();
            if (applicationUsers.LastMustChangedPasswordDate == null || (applicationUsers.MustChangePassword != null && applicationUsers.MustChangePassword == true)
                || (applicationUsers.LastMustChangedPasswordDate != null && (DateTime.Now - applicationUsers.LastMustChangedPasswordDate.Value).Days > 90)
                || applicationUsers.LastResetPasswordDate > applicationUsers.LastMustChangedPasswordDate)
            {
                context.Fail();
            }
            else
            {
                context.Succeed(requirement);
            }
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
