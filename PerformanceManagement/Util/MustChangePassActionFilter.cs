using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.Models;
using PerformanceManagement.Models.HRAdmin;
using PerformanceManagement.Models.ICTAdmin;
using PerformanceManagement.ViewModels;

namespace PerformanceManagement.Util
{
    public class MustChangePassActionFilter : IAsyncActionFilter
    {
        async System.Threading.Tasks.Task IAsyncActionFilter.OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //string actionName = context.ActionDescriptor.DisplayName;
            var userName = context.HttpContext.User.Identity.Name;
            string actionName = (string)context.RouteData.Values["Action"];
            if (actionName != "MustChangePassword" && actionName != "GetCurrentUserInfo3" && actionName != "Logout")
            {
                MustChangePassControlViewModel mustChangePassControlViewModel = new MustChangePassControlViewModel();
                mustChangePassControlViewModel.UserName = null;
                AppDbContext appDbContext = (AppDbContext)context.HttpContext.RequestServices.GetService(typeof(AppDbContext));

                ApplicationUser applicationUsers = appDbContext.applicationUsers.Where(c => c.UserName == userName).SingleOrDefault();
                if (applicationUsers != null)
                {
                    if (applicationUsers.LastMustChangedPasswordDate == null || (applicationUsers.MustChangePassword != null && applicationUsers.MustChangePassword == true)
                        || (applicationUsers.LastMustChangedPasswordDate != null && (DateTime.Now - applicationUsers.LastMustChangedPasswordDate.Value).Days > 90)
                        || applicationUsers.LastResetPasswordDate > applicationUsers.LastMustChangedPasswordDate)
                    {
                        //context.Result = new RedirectToRouteResult(
                        //    new RouteValueDictionary(new { controller = "MustChangePassword", action = "Profile" })
                        //    );
                        context.HttpContext.Response.Redirect("/Profile/MustChangePassword");
                    }
                }

            }

            var result = await next();
        }
    }
}
